using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private void OnEnable()
    {        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemyDefeated += CheckEnemyDefeatAchievement;
        }
        else
        {
            Debug.LogWarning("AchievementManager: GameManager.Instance is null in OnEnable. Subscription skipped.");
        }
    }

    private void OnDisable()
    {     
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemyDefeated -= CheckEnemyDefeatAchievement;
        }
        else
        {
            Debug.LogWarning("AchievementManager: GameManager.Instance is null in OnDisable. Unsubscription skipped.");
        }
    }

    private void CheckEnemyDefeatAchievement(string defeatedEnemyName)
    {
        Debug.Log($"Achievement: '{defeatedEnemyName}' defeated! Checking for related achievements.");
        if (defeatedEnemyName == "Ancient Dragon")
        {
            Debug.Log("CONGRATULATIONS! You've unlocked the 'Dragon Slayer' achievement!");
        }
    }
}