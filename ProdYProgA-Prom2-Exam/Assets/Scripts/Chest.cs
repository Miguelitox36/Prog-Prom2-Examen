using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public string Content = "Experience, potion and mana";
    private bool hasBeenOpened = false;

    public void Interact(Player player)
    {
        if (hasBeenOpened)
        {
            Debug.Log($"The {gameObject.name} is empty. You've already opened it.");
            return;
        }

        Debug.Log($"{player.Name} opens the {gameObject.name} and finds {Content}!");
                
        player.GainExperience(10);
                
        player.RestoreHealth(10);
               
        player.RestoreMana(15);

        Debug.Log($"{player.Name} gained 10 experience, 10 health, and 15 mana! Current XP: {player.Experience}, Current Health: {player.Health}, Current Mana: {player.Mana}");

        hasBeenOpened = true;
    }
}