using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string Name;
    public int Health;
    public int Attack;
    public IAttackStrategy AttackStrategy;

    protected virtual void Awake()
    {
        AttackStrategy = new MeleeAttack();
    }

    public virtual void PerformAttack(Character target)
    {
        if (AttackStrategy != null)
        {
            AttackStrategy.ExecuteAttack(this, target);
        }
        else
        {
            Debug.Log($"{Name} attacks {target.Name} for {Attack} damage!");
            target.TakeDamage(Attack);
        }
    }    
    public abstract void TakeDamage(int damage);
}