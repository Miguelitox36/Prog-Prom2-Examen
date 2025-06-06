using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : IAttackStrategy
{
    public int ManaCost;

    public MagicAttack(int cost)
    {
        ManaCost = cost;
    }

    public void ExecuteAttack(Character attacker, Character target)
    {
        int magicDamage = attacker.Attack * 2;
        Debug.Log($"{attacker.Name} casts a spell at {target.Name} for {magicDamage} magic damage (consumes {ManaCost} mana).");
                
        target.TakeDamage(magicDamage);
    }
}