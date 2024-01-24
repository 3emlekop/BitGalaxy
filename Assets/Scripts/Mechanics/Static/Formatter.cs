using System.Collections.Generic;

public class Formatter<T>
{
    public static T[] ArrayCompress(T[] array, bool leaveStartLength)
    {
        var temp = new List<T>();
        foreach (var item in array)
        {
            if(item != null)
                temp.Add(item);
        }

        if(leaveStartLength)
        {
            T[] newArray = new T[array.Length];
            for(byte i = 0; i < temp.Count; i++)
                newArray[i] = temp[i];

            return newArray;
        }
        else
            return temp.ToArray();
    }
}
