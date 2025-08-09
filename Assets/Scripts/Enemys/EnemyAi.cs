using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
  
    [SerializeField]
    private float attackRange = 3f;
    [SerializeField]
    private float awarenessRadius = 10f;

    public bool isAggro { get; private set; }
    public bool IsWalking { get; private set; }

    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "Attack";


    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;


    [SerializeField]
    private Animator animator;


    private void Start()
    {
        SetComponents();
        enemyNavMeshAgent.stoppingDistance = attackRange;
        enemyNavMeshAgent.radius = 2f; 
        
    }

    private void Update()
    {

        if (playersTransform == null) return; 

        float dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < awarenessRadius)
        {
            isAggro = true;
        }


        if (isAggro && dist > attackRange)
        {
       
            enemyNavMeshAgent.SetDestination(playersTransform.position);

        }


        if (dist <= attackRange)
        {
            AttackPlayer();
        }


        IsWalking = enemyNavMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool(IS_WALKING, IsWalking);

    }


    private void AttackPlayer()
    {
        enemyNavMeshAgent.ResetPath();
        animator.SetTrigger(IS_ATTACKING);
    }


     private void setComponents()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }


    }


    public void SetAggro(bool value)
    {
        isAggro = value;
    }



    private void SetComponents()
    {
        playersTransform = FindFirstObjectByType<PlayerMovment>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }
}