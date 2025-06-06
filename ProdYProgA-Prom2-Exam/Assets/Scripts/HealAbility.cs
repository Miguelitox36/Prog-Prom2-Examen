using UnityEngine;

public class HealAbility : Ability
{
    public int BaseHeal { get; set; }

    protected override void Awake()
    {
        base.Awake();
        AbilityName = "Heal Wounds";
        ManaCost = 15;
        BaseHeal = 20;
    }

    public override void Use(Character user, Character target)
    {
        Debug.Log($"{user.Name} uses {AbilityName} on {target.Name}!");
        target.Health += BaseHeal;
        Debug.Log($"{target.Name} has been healed by {BaseHeal}. Current Health: {target.Health}");
    }
}