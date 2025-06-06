using GameJolt.API;
using UnityEngine;

public class InteractableComponent : MonoBehaviour, IInteractable
{
    private IInteractable interactableObject;

    void Awake()
    {        
        NPC npc = GetComponent<NPC>();
        if (npc != null)
        {
            interactableObject = npc;
            Debug.Log($"InteractableComponent found NPC: {npc.Name}");
            Trophies.TryUnlock(269987);
            return;
        }

        Chest chest = GetComponent<Chest>();
        if (chest != null)
        {
            interactableObject = chest;
            Debug.Log($"InteractableComponent found Chest: {chest.name}");
            Trophies.TryUnlock(269986);
            return;
        }        
        Debug.LogWarning($"InteractableComponent on {gameObject.name}: No interactable component found!");
    }

    public void Interact(Player player)
    {
        if (interactableObject != null)
        {
            interactableObject.Interact(player);
        }
        else
        {
            Debug.LogWarning($"No interactable object assigned to {gameObject.name}");
        }
    }
}