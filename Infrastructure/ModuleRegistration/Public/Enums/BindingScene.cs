using System;

namespace ModuleRegistration.Public
{
    [Flags]
    public enum BindingScene : byte
    {
        All = 1,
        Entry = 2,
        Menu = 4,
        Gameplay = 8
    }
}