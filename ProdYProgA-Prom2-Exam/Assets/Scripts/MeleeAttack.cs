using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttackStrategy
{
    public void ExecuteAttack(Character attacker, Character target)
    {
        int damage = attacker.Attack;
        Debug.Log($"{attacker.Name} performs a melee attack on {target.Name} for {damage} damage.");       
        target.TakeDamage(damage);
    }
}