using System;
public class Define
{
    public static T RandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(new Random().Next(0,values.Length));
    }
}
