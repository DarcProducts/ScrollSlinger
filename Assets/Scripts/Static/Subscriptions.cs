using UnityEngine;

public static class Subscriptions
{
    [Range(0f, 1f)] public static float chanceForSubscriber;

    public static bool IsASubscriber()
    {
        if (Random.value < chanceForSubscriber)
            return true;
        return false;
    }
}
