using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public event Action OnEnemyDeath;

    [Header("Health Settings")]
    [SerializeField] private float enemyHealth = 50;

    [Header("Effects")]
    [SerializeField] private GameObject gunhitEffect;

    [Header("Components")]
    [SerializeField] private Animator animator;


    private Collider[] colliders;
    private NavMeshAgent navAgent;
    private EnemyAi enemyAI;
    private bool isDying = false;
    private const string IS_DEAD = "IsDead";

    void Start()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        colliders = GetComponentsInChildren<Collider>();
        navAgent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAi>();
    }

    void Update()
    {
        if (enemyHealth <= 0 && !isDying)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDying) return;

        if (gunhitEffect != null)
        {
            Instantiate(gunhitEffect, transform.position, Quaternion.identity);
        }

        enemyHealth = Mathf.Max(enemyHealth - damage, 0);
        Debug.Log($"Enemy {name} took {damage} damage. Remaining health: {enemyHealth}");
    }

    private void Die()
    {
        if (isDying) return;
        isDying = true;

        Debug.Log($"Enemy {name} has been defeated.");
        DifficultyManager.Instance.OnEnemyKilled();
        DisableEnemyComponents();
        OnEnemyDeath?.Invoke();

        if (EnemyManager.Instance != null)
            EnemyManager.Instance.RemoveEnemy(this);

        if (animator != null)
        {
            animator.SetBool(IS_DEAD, true);
            StartCoroutine(WaitForDeathAnimation());
        }
        else
        {
           
            HandleLootDrop();
            Destroy(gameObject);
        }
    }

    private void DisableEnemyComponents()
    {
        foreach (Collider col in colliders)
        {
            if (col != null)
                col.enabled = false;
        }

        if (navAgent != null)
        {
            navAgent.enabled = false;
        }

        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }

        gameObject.tag = "Untagged";
    }

    private void HandleLootDrop()
    {
        Debug.Log($"[Enemy] HandleLootDrop called for {name}");

        EnemyDrops dropComponent = GetComponent<EnemyDrops>();
        if (dropComponent != null)
        {
            Debug.Log($"[Enemy] Found EnemyDrops component, calling DropLoot()");
            dropComponent.DropLoot();
        }
        else
        {
            Debug.LogWarning($"[Enemy] No EnemyDrops component found on {name}!");
        }
    }

    IEnumerator WaitForDeathAnimation()
    {
        Debug.Log($"[Enemy] Starting death animation coroutine for {name}");

        // Wait for death animation to start
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float timeout = 0f;

        // Wait for Death state to begin (with timeout to prevent infinite loop)
        while (!stateInfo.IsName("Death") && timeout < 2f)
        {
            yield return null;
            timeout += Time.deltaTime;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // If we found the death animation, wait for it to complete
        if (stateInfo.IsName("Death"))
        {
            Debug.Log($"[Enemy] Playing death animation for {name}");

            while (stateInfo.normalizedTime < 0.95f) // 0.95 instead of 1.0 for safety
            {
                yield return null;
                stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                // Break if state changed unexpectedly
                if (!stateInfo.IsName("Death"))
                    break;
            }
        }
        else
        {
            Debug.LogWarning($"[Enemy] Death animation state not found for {name}, using fallback delay");
            
            yield return new WaitForSeconds(1f);
        }

     
        HandleLootDrop();

       
        yield return new WaitForSeconds(0.1f);

        Debug.Log($"[Enemy] Destroying {name}");
   
        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return !isDying && enemyHealth > 0;
    }
}