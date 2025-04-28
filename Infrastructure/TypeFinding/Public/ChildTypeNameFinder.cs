using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeFinding.Public
{
    public class ChildTypeNameFinder
    {
        public static bool TryGetChildNames(Type baseType, out IEnumerable<string> result,
            FindType findType = FindType.None, Assembly assembly = null, params Type[] ignoreTypes)
        {
            result = null;
            if (!ChildTypeFinder.TryGetChildTypes(baseType, out var types, findType, assembly, ignoreTypes)) return false;

            result = new List<string>(types.Select(type => type.Name));

            return true;
        }
    }
}