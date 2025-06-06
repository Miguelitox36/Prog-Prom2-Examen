using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Player player; 
    public Enemy enemy;   

    void Start()
    {       
        player.AttackStrategy = new MeleeAttack(); 
                
        enemy.AttackStrategy = new MeleeAttack();
                
        Invoke("ChangePlayerStrategy", 5f); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.PerformAttack(enemy);
        }
    }

    void ChangePlayerStrategy()
    {
        Debug.Log("Player changes attack strategy to Magic Attack!");
        player.AttackStrategy = new MagicAttack(15); 
    }
}