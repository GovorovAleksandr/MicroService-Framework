using System;
using System.Collections.Generic;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using MonoReferencing.Public;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MonoReferencing.Core
{
    internal sealed class ReferenceInstaller : IInitializable
    {
        private const string ErrorFormat =
            "The data from {0} has not been installed because data of this type has already been installed by another object";
        
        [Inject] private readonly IEventBus _eventBus;
        
        private readonly List<Type> _installedTypes = new List<Type>();
        
        public void Initialize()
        {
            var monoReferences = Object.FindObjectsOfType<MonoReference>(true);

            foreach (var monoReference in monoReferences)
            {
                var monoReferenceType = monoReference.GetType();

                while ((monoReferenceType.IsGenericType
                           ? monoReferenceType.GetGenericTypeDefinition()
                           : monoReferenceType) != typeof(MonoReference<>))
                {
                    monoReferenceType = monoReferenceType.BaseType;
                }
                
                var dataType = monoReferenceType.GenericTypeArguments[0];
                
                if (_installedTypes.Contains(dataType))
                {
                    Debug.LogError(string.Format(ErrorFormat, dataType.FullName));
                    return;
                }
                
                _eventBus.Send(new MonoReferenceLoaded(dataType, monoReference.Data));
                if (monoReference.AllowMultiple) continue;
                _installedTypes.Add(dataType);
            }
        }
    }
}