using System;

namespace Heliconia.Domain
{
    public static class CloneEntity
    {
        public static T Clone<T>(T obj) where T : ICloneable
        {
            return (T)obj.Clone();
        }
    }
}
