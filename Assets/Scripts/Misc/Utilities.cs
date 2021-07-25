using System;

//Return every enum entries of a specific type
public static class EnumUtil
{
    public static T[] GetValues<T>()
    {
        return (T[])Enum.GetValues(typeof(T));
    }
}
