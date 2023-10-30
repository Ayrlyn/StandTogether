using System.Collections.Generic;

public static class ListExtensionMethods
{
    public static T GetRandomElement<T>(this List<T> list)
    {
        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        return list[randomIndex];
    }
    public static bool IsEmpty<T>(this List<T> list)
    {
        return list.Count == 0;
    }
}
