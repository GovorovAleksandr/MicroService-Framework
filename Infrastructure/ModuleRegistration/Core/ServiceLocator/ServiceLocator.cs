using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModuleRegistration.Public;
using ModuleRegistration.Utils;
using UnityEngine;

namespace ModuleRegistration.Core
{
    [MultipleContext]
    internal sealed class ServiceLocator : IServiceLocatorInternal
    {
        private readonly Dictionary<Type, ServiceContainer> _localServices = new Dictionary<Type, ServiceContainer>();
        private readonly HashSet<object> _toInject = new HashSet<object>();

        private readonly IServiceLocatorInternal _parentLocator;

        internal ServiceLocator(IServiceLocatorInternal parentLocator = null)
        {
            _parentLocator = parentLocator;
            
            _localServices[typeof(IServiceLocator)] = new ServiceContainer(this, ServiceBindingFlags.Single);
            
            var serviceInjector = new ServiceInjector(this, _toInject);
            _localServices[typeof(IServiceInjectorInternal)] = new ServiceContainer(serviceInjector, ServiceBindingFlags.Single);
            _localServices[typeof(IServiceInjector)] = new ServiceContainer(serviceInjector, ServiceBindingFlags.Single);
        }

        IDictionary<Type, ServiceContainer> IServiceLocatorInternal.Services => _localServices;
        IDictionary<Type, ServiceContainer> IServiceLocatorInternal.AllServices => GetAllServices();
        
        public void AddAllFromNew<T>(Func<T> factory, ServiceBindingFlags flag = Constants.DefaultServiceBindingFlag)
        {
            var service = factory();
            AddAll(service, flag);
        }

        public void AddAllFromNew(Type keyType, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var service = factory();
            if (service.GetType() != keyType) throw new Exception($"Factory must return an instance of type {keyType}");
            AddAll(service, flags);
        }

        public void AddInterfacesFromNew<T>(Func<T> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var service = factory();
            AddInterfaces(service, flags);
        }

        public void AddInterfacesFromNew(Type keyType, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var service = factory();
            if (service.GetType() != keyType) throw new Exception($"Factory must return an instance of type {keyType}");
            AddInterfaces(service, flags);
        }

        public void AddInstanceFromNew<T>(Func<T> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var keyType = typeof(T);
            if (!keyType.IsClass && !keyType.IsValueType) throw new Exception("Class or structure must be passed as a parameter T");
            var service = factory();
            AddInstance(service, flags);
        }

        public void AddInstanceFromNew(Type keyType, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var service = factory();
            if (service.GetType() != keyType) throw new Exception($"Factory must return an instance of type {keyType}");
            AddInstance(service, flags);
        }

        public void AddAsFromNew<TKey, TInstance>(Func<TInstance> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var keyType = typeof(TKey);
            var service = factory();
            AddAs(keyType, service, flags);
        }

        public void AddAsFromNew(Type keyType, Type serviceType, Func<object> factory, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            var service = factory();
            if (service.GetType() != keyType) throw new Exception($"Factory must return an instance of type {keyType}");
            AddAs(keyType, service, flags);
        }

        public void AddAll(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            AddInstance(service, flags);
            AddInterfaces(service, flags);
        }

        public void AddInterfaces(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            _toInject.Add(service);
            var serviceType = service.GetType();
            
            foreach (var iface in serviceType.GetInterfaces())
            {
                if (Constants.IgnoredByServiceLocatorTypes.Contains(iface)) continue;
                if (iface.GetCustomAttribute(typeof(ForceMultiple)) != null)
                {
                    AddMultipleService(iface, service);
                    continue;
                }

                Add(iface, service, flags);
            }
        }

        public void AddInstance(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            _toInject.Add(service);
            var keyType = service.GetType();
            Add(keyType, service, flags);
        }

        public void AddAs<TKey>(object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            _toInject.Add(service);
            var keyType = typeof(TKey);
            AddAs(keyType, service, flags);
        }

        public void AddAs(Type keyType, object service, ServiceBindingFlags flags = Constants.DefaultServiceBindingFlag)
        {
            _toInject.Add(service);
            Add(keyType, service, flags);
        }

        public T Get<T>()
        {
            var type = typeof(T);
            return (T)Get(type);
        }

        public object Get(Type type)
        {
            if (GetAllServices().TryGetValue(type, out var serviceContainer))
                return serviceContainer.Service;

            var impl = GetAllServices().FirstOrDefault(s => type.IsAssignableFrom(s.Key)).Value;
            if (impl != null) return impl.Service;
            
            throw new Exception($"The {type.FullName} service does not exist");
        }

        public bool TryGet<T>(out T service)
        {
            service = default;
            if (!TryGet(typeof(T), out var rawService)) return false;
            
            service = (T)rawService;
            
            return true;
        }

        public bool TryGet(Type serviceType, out object service)
        {
            service = null;
            if (!GetAllServices().TryGetValue(serviceType, out var serviceContainer)) return false;

            service = serviceContainer.Service;
            
            return true;
        }

        public void InitializeAll()
        {
            if (!TryGet(typeof(IInitializable), out var services)) return;
			
            var initializables = (IList<object>)services;
			
            foreach (var service in initializables)
            {
                if (!(service is IInitializable initializable)) continue;
					
                initializable.Initialize();
            }
        }

        public void DisposeAll()
        {
            foreach (var pair in _localServices)
            {
                var serviceContainer = pair.Value;
                var flags = serviceContainer.BindingFlags;
                
                switch (flags)
                {
                    case ServiceBindingFlags.Single:
                        var service = serviceContainer.Service;
                        Dispose(service);
                        break;
                    case ServiceBindingFlags.Multiple:
                        var services = serviceContainer.Service;
                        MultipleDispose(services);
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(flags), flags, null);
                }
            }
        }

        private void Add(Type keyType, object service, ServiceBindingFlags flags)
        {
            if (keyType.GetCustomAttribute(typeof(IgnoredByServiceLocatorAttribute)) != null) return;
            
            GetAllServices().TryGetValue(keyType, out var serviceContainer);
            
            switch (flags)
            {
                case ServiceBindingFlags.Single:
                    if (serviceContainer != null) throw new Exception($"The service {keyType.FullName} has already been added to the ServiceLocator");
                    AddSingleService(keyType, service);
                    break;
                case ServiceBindingFlags.Multiple:
                    if (serviceContainer != null && flags != serviceContainer.BindingFlags)
                        throw new Exception(
                            $"For type {keyType.FullName}, there already exists a flag with a value ({serviceContainer.BindingFlags}) that does not match the current one ({flags})");
                    AddMultipleService(keyType, service);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(flags), flags, null);
            }
            
            var contextName = _parentLocator == null ? "Global" : "Local";
            Debug.Log($"[{contextName} Context]: [{service.GetType().FullName}] registered as [{keyType.FullName}]");
        }

        private void AddSingleService(Type serviceType, object service)
        {
            _localServices[serviceType] = new ServiceContainer(service, ServiceBindingFlags.Single);
        }

        private void AddMultipleService(Type serviceType, object service)
        {
            if (!_localServices.TryGetValue(serviceType, out var serviceContainer))
            {
                _localServices[serviceType] = serviceContainer =
                    new ServiceContainer(new List<object>(), ServiceBindingFlags.Multiple);
            }
            
            ((List<object>)serviceContainer.Service).Add(service);
        }

        private static void Dispose(object service)
        {
            if (!(service is IDisposable disposable)) return;
            
            disposable.Dispose();
        }

        private static void MultipleDispose(object service)
        {
            ((List<object>)service).ForEach(Dispose);
        }

        private Dictionary<Type, ServiceContainer> GetAllServices() =>
            _localServices.Union(_parentLocator != null
                    ? _parentLocator.AllServices.Where(serviceContainer =>
                        serviceContainer.Key.GetCustomAttribute(typeof(MultipleContextAttribute)) == null)
                    : Enumerable.Empty<KeyValuePair<Type, ServiceContainer>>())
                .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}