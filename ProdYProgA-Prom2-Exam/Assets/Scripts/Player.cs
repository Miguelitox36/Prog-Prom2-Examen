using UnityEngine;

public class Player : Character
{
    public int Experience;
    public int Mana;
    private float lastAttackTime = 0f;
    private float attackCooldown = 1f;

    protected override void Awake()
    {
        base.Awake();
        Name = "Hero";
        Health = 150;
        Attack = 25;
        Experience = 0;
        Mana = 50;
        Debug.Log($"Player {Name} has appeared. Initial Mana: {Mana}");
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{Name} took {damage} damage. Current Health: {Health}");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdatePlayerHealth(Health);
        }

        if (Health <= 0)
        {
            Debug.Log($"{Name} has been defeated!");
        }
    }

    public new void PerformAttack(Character target)
    {
        if (AttackStrategy != null)
        {
            AttackStrategy.ExecuteAttack(this, target);
        }
        else
        {
            base.PerformAttack(target);
        }
        Debug.Log($"{Name} performs a player-specific action during attack!");
    }

    public void GainExperience(int amount)
    {
        Experience += amount;
        Debug.Log($"{Name} gained {amount} experience. Total: {Experience}");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.NotifyPlayerExperienceChanged(Experience);
        }
    }

    public void RestoreMana(int amount)
    {
        Mana += amount;
        Debug.Log($"{Name} restored {amount} mana. Current Mana: {Mana}");
    }

    public void RestoreHealth(int amount)
    {
        Health += amount;
        Debug.Log($"{Name} restored {amount} health. Current Health: {Health}");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdatePlayerHealth(Health);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemy.Health > 0)
            {
                // Control de cooldown para evitar spam de ataques
                bool canAttack = Time.time - lastAttackTime >= attackCooldown;

                if (Input.GetKeyDown(KeyCode.X) && canAttack)
                {
                    int magicCost = 8;
                    if (Mana >= magicCost)
                    {
                        IAttackStrategy originalStrategy = AttackStrategy;
                        AttackStrategy = new MagicAttack(magicCost);
                        PerformAttack(enemy);
                        AttackStrategy = originalStrategy;

                        Mana -= magicCost;
                        lastAttackTime = Time.time;
                        Debug.Log($"Player {Name} used Magic Attack. Mana remaining: {Mana}");
                    }
                    else
                    {
                        Debug.Log($"{Name} doesn't have enough mana for Magic Attack! (Need {magicCost}, have {Mana})");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.C) && canAttack)
                {
                    PerformAttack(enemy);
                    lastAttackTime = Time.time;
                    Debug.Log($"Player {Name} used Melee Attack.");
                }

                // Verificar si el enemigo fue derrotado
                if (enemy.Health <= 0)
                {
                    Debug.Log($"{enemy.Name} has been defeated by {Name}!");
                    GainExperience(15);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Player {Name} is no longer in contact with {other.name}.");
        }
    }

    void Update()
    {
        if (Health <= 0)
        {
            Debug.Log($"{Name} has been defeated! Game Over.");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PushState("GameOver");
            }
            enabled = false;
        }
    }
}