using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeFinding.Public
{
    public static class ChildTypeFinder
    {
        public static bool TryGetChildTypes(Type baseType, out IEnumerable<Type> result,
            FindType findType = FindType.None, Assembly assembly = null, params Type[] ignoreTypes)
        {
            result = null;
            if (baseType == null) return false;
            
            IEnumerable<Type> types = new HashSet<Type>();
            
            types = assembly == null ?
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) :
                assembly.GetTypes();
            
            var foundTypes = new HashSet<Type>();
            
            foreach (var type in types)
            {
                if (ignoreTypes.Length > 0)
                    if (ignoreTypes.Any(ignoreType => ignoreType.IsAssignableFrom(type)))
                        continue;

                if (!baseType.IsAssignableFrom(type))
                {
                    var allNone = true;
                    
                    foreach (var @interface in type.GetInterfaces())
                    {
                        var genericInterface = @interface.IsGenericType ? @interface.GetGenericTypeDefinition() : @interface;
                        
                        if (@interface != baseType && genericInterface != baseType) continue;
                        allNone = false;
                        break;
                    }

                    if (allNone) continue;
                }
                
                if (!findType.HasFlag(FindType.IncludeBaseType) && type == baseType) continue;
                if (!findType.HasFlag(FindType.IncludeAbstractTypes) && type.IsAbstract) continue;
                if (!findType.HasFlag(FindType.IncludeInterfaceTypes) && type.IsInterface) continue;
                
                foundTypes.Add(type);
            }
            
            result = foundTypes;
            return foundTypes.Any();
        }
    }
}