using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModuleRegistration.Public;
using UnityEngine;

namespace ModuleRegistration.Core
{
    [MultipleContext]
	internal sealed class ServiceInjector : IServiceInjectorInternal
	{
		private readonly IServiceLocatorInternal _locator;
        private readonly IEnumerable<object> _toInject;

        public ServiceInjector(IServiceLocatorInternal locator, IEnumerable<object> toInject)
        {
            _locator = locator;
            _toInject = toInject;
        }

        public void InjectAll()
        {
            foreach (var service in _toInject)
            {
                Inject(service);
            }
        }

        public void Inject(object service)
		{
			var instanceType = service.GetType();
            
            foreach (var field in instanceType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (field.GetCustomAttributes(typeof(InjectAttribute), true).Length == 0) continue;
                if (field.GetValue(service) != null) continue;

                var dependencyType = field.FieldType;
                
                try
                {
                    if (dependencyType.IsGenericType && dependencyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        InjectMultipleField(service, field);
                        continue;
                    }

                    InjectSingleField(service, field);

                    var locatorName = _locator.AllServices.Count > _locator.Services.Count ? "Local" : "Global";
                    Debug.Log($"[{locatorName} Injector]: injected [{dependencyType.FullName}] to [{instanceType.FullName}]");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to inject {dependencyType.FullName} at {instanceType.FullName}: {ex}");
                }
            }
        }

        private void InjectMultipleField(object instance, FieldInfo field)
        {
            var dependencyType = field.FieldType;
            var dependencyElementType = dependencyType.GetGenericArguments()[0];

            var serviceContainer = Resolve(dependencyType, dependencyElementType);
            var serviceList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(dependencyElementType));
            var elements = (IEnumerable)serviceContainer.Service;

            foreach (var service in elements)
            {
                if (!dependencyElementType.IsInstanceOfType(service)) continue;
                serviceList.Add(service);
            }
            
            field.SetValue(instance, serviceList);
        }

        private void InjectSingleField(object instance, FieldInfo field)
        {
            var instanceType = instance.GetType();
            var dependencyType = field.FieldType;
            
            var service = Resolve(instanceType, dependencyType).Service;
            
            field.SetValue(instance, service);
        }

        private ServiceContainer Resolve(Type instanceType, Type dependencyType)
        {
            var serviceContainer = _locator.AllServices.FirstOrDefault(service => dependencyType == service.Key);
            
            if (serviceContainer.Key == null)
                throw new InvalidOperationException($"Failed to resolve field {dependencyType.FullName} in {instanceType.FullName}");
            
            return serviceContainer.Value;
        }
	}
}