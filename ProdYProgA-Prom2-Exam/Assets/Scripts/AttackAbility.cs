using UnityEngine;

public class AttackAbility : Ability
{
    public int BaseDamage { get; set; }

    protected override void Awake()
    {
        base.Awake();
        AbilityName = "Powerful Strike";
        ManaCost = 0; 
        BaseDamage = 10;
    }

    public override void Use(Character user, Character target)
    {
        Debug.Log($"{user.Name} uses {AbilityName} on {target.Name}!");
        target.Health -= BaseDamage + user.Attack;
        if (target.Health <= 0)
        {
            Debug.Log($"{target.Name} has been defeated.");
        }
        else
        {
            Debug.Log($"{target.Name} now has {target.Health} health.");
        }
    }
}