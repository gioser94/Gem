using System;


public static class EnumUtil
{
    public static T[] GetValues<T>()
    {
        return (T[])Enum.GetValues(typeof(T));
    }
}
