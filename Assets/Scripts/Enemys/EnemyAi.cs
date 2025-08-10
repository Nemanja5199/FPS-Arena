using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
  
    [SerializeField]
    private float attackRange = 3f;
    [SerializeField]
    private float awarenessRadius = 10f;
    private float attackCooldown = 1.5f; 
    [SerializeField] 
    private int attackDamage = 10;

    private float nextAttackTime = 0f;


    [SerializeField] private float roamRadius = 10f;    
    [SerializeField] private float roamDelay = 2f;
    private Vector3 startPosition;
    private float nextRoamTime = 0f;

    [SerializeField]
    private bool enableDebug = false;


    [SerializeField] 
    private LayerMask attackMask;

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

        startPosition = transform.position;
        
    }

    private void Update()
    {

        if (playersTransform == null) return; 

        float dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < awarenessRadius)
            isAggro = true;
        else if (dist > awarenessRadius * 1.5f)
            isAggro = false;


        if (isAggro && dist > attackRange)
        {
            
            enemyNavMeshAgent.SetDestination(playersTransform.position);

        }

        if (!isAggro)
        {
            enemyNavMeshAgent.stoppingDistance = 0.1f;
            Roam();
            return; 
        }
        else
        {
            enemyNavMeshAgent.stoppingDistance = attackRange;
        }



       
        if (dist > attackRange)
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        else
            AttackPlayer();


        IsWalking = enemyNavMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool(IS_WALKING, IsWalking);

    }


    private void AttackPlayer()
    {
        enemyNavMeshAgent.ResetPath();
        if (Time.time < nextAttackTime) return;


        Vector3 dirToPlayer = (playersTransform.position - transform.position).normalized;
        if(Physics.Raycast(transform.position,dirToPlayer,out  RaycastHit hit,attackRange, attackMask))
        {
            if (enableDebug)
            {
                Debug.Log($"Raycast hit: {hit.transform.name}, Layer: {LayerMask.LayerToName(hit.transform.gameObject.layer)}");
            }
           
            if (hit.transform.root == playersTransform)
            {
                animator.SetTrigger(IS_ATTACKING);


                hit.transform.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
                nextAttackTime = Time.time + attackCooldown;
            }
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




    private Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }


    private void Roam()
    {
      
        if (Time.time >= nextRoamTime)
        {
            Vector3 roamTarget = GetRandomPoint(startPosition, roamRadius);
            enemyNavMeshAgent.SetDestination(roamTarget);
            nextRoamTime = Time.time + roamDelay;
        }
        IsWalking = enemyNavMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool(IS_WALKING, IsWalking);
    }

}