using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public int HealingAmount;

    public Potion(string name, string description, int healingAmount) : base(name, description)
    {
        HealingAmount = healingAmount;
    }
}