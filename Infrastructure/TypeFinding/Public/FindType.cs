using System;

namespace TypeFinding.Public
{
    [Flags]
    public enum FindType : byte
    {
        None = 0,
        IncludeBaseType = 1,
        IncludeAbstractTypes = 2,
        IncludeInterfaceTypes = 4
    }
}