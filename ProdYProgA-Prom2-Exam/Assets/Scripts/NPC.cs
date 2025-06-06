using UnityEngine;

public class NPC : Character, IInteractable
{
    public string Dialogue = "Hello, adventurer! Are you looking for a quest?";
    private bool hasBeenInteracted = false;

    protected override void Awake()
    {
        base.Awake();
        Name = "Villager";
        Health = 100;
        Attack = 0;
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{Name} took {damage} damage. Current Health: {Health}");
        if (Health <= 0)
        {
            Debug.Log($"{Name} has been defeated!");
        }
    }

    public void Interact(Player player)
    {
        if (hasBeenInteracted)
        {
            Debug.Log($"{Name}: We've already spoken, adventurer. Good luck!");
            return;
        }

        Debug.Log($"{Name}: {Dialogue}");
               
        player.GainExperience(15);
               
        player.RestoreMana(10);

        Debug.Log($"{player.Name} gained 15 experience and 10 mana from talking to {Name}! Current XP: {player.Experience}, Current Mana: {player.Mana}");

        hasBeenInteracted = true;
    }
}