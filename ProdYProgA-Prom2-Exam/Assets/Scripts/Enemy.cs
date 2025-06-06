using GameJolt.API;
using UnityEngine;

public class Enemy : Character
{
    public string EnemyType;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    private Transform playerTransform;
    private bool isInCombat = false;
    private float lastAttackTime = 0f;
    private float attackCooldown = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        Name = "Goblin";
        Health = 30;
        Attack = 15;
        EnemyType = "Beast";
        Debug.Log($"Enemy {Name} ({EnemyType}) has appeared.");

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerTransform = player.transform;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;        
        Debug.Log($"{Name} took {damage} damage. Current Health: {Health}");

        if (Health <= 0)
        {
            Debug.Log($"{Name} has been defeated!");            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DefeatEnemy(Name, Health);
            }
            Destroy(gameObject);
            Trophies.TryUnlock(269988);
        }
    }

    void Update()
    {
        if (isInCombat && playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                transform.LookAt(playerTransform);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInCombat = true;
            Debug.Log($"{Name} enters combat with {other.GetComponent<Player>().Name}!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isInCombat)
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.Health > 0)
            {                
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    PerformAttack(player);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Enemy {Name} is no longer in contact with player.");
            isInCombat = false;
        }
    }
}