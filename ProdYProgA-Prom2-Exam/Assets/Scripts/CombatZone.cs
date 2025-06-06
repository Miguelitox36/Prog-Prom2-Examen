using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public string ZoneName = "Enchanted Forest"; 
    public List<Enemy> PresentEnemies; 

    void Awake()
    {
        if (PresentEnemies == null)
        {
            PresentEnemies = new List<Enemy>();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        PresentEnemies.Add(enemy);
        Debug.Log($"{enemy.Name} has appeared in {ZoneName}.");
    }

    public void DisplayEnemies()
    {
        Debug.Log($"Enemies in {ZoneName}:");
        if (PresentEnemies.Count == 0)
        {
            Debug.Log("No enemies in this zone.");
            return;
        }
        foreach (var enemy in PresentEnemies)
        {
            if (enemy != null) 
            {
                Debug.Log($"- {enemy.Name} (Health: {enemy.Health})");
            }
        }
    }
}