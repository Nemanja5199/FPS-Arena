using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]
    private Material aggroMat;
    [SerializeField]
    private Material normalMat;
    [SerializeField]
    private float attackRange = 3f;

    public bool isAggro = false;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private MeshRenderer meshRenderer;

    [SerializeField]
    private float awarenessRadius = 10f; 

    private void Start()
    {
        playersTransform = FindFirstObjectByType<PlayerMovment>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();


        enemyNavMeshAgent.stoppingDistance = attackRange;

        enemyNavMeshAgent.radius = 2f; 
        if (normalMat == null)
            normalMat = meshRenderer.material;
    }

    private void Update()
    {

        if (playersTransform == null) return; 

        float dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < awarenessRadius)
        {
            isAggro = true;
        }

        
        if (isAggro)
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
            meshRenderer.material = aggroMat;
        }
        else
        {
            meshRenderer.material = normalMat;
           
        }
    }
}