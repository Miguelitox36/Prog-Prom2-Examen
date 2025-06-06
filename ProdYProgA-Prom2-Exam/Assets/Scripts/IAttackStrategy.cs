using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    void ExecuteAttack(Character attacker, Character target);
}