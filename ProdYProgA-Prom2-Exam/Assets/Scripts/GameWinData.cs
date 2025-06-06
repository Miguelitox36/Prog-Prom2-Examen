using UnityEngine;

public static class GameWinData
{
    private static int finalHealth = 0;
    private static float completionTime = 0f;

    public static void Save(int health, float time)
    {
        finalHealth = health;
        completionTime = time;
        Debug.Log($"GameWinData saved: Health={health}, Time={time:F2}s");
    }

    public static (int health, float time) Load()
    {
        return (finalHealth, completionTime);
    }

    public static void Clear()
    {
        finalHealth = 0;
        completionTime = 0f;
    }
}
