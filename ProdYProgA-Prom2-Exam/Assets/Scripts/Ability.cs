using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string AbilityName { get; set; }
    public int ManaCost { get; set; }

    protected virtual void Awake()
    {
        
    }

    public abstract void Use(Character user, Character target); 
}