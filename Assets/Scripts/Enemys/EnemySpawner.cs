using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject[] enemyFabs;
    [SerializeField] private int maxEnemis = 20;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private float minDistanceFromPlayer = 8f;
    [SerializeField] private int maxSpawnAttempts = 20;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask visionBlockMask;

    [Header("Debug")]
    [SerializeField] private bool enableDebug = false;
    [SerializeField] private float debugDrawDuration = 2f;

    private float spawnTimer;
    private int currentEnemies;

    void Start()
    {
        ValidateSetup();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (currentEnemies < maxEnemis && spawnTimer >= spawnInterval)
        {
            if (enableDebug)
                Debug.Log($"[{Time.time:F2}] Spawning enemy... (Current: {currentEnemies}/{maxEnemis})");

            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // Quick validation
        if (player == null || enemyFabs == null || enemyFabs.Length == 0)
        {
            if (enableDebug)
                Debug.LogError("Cannot spawn: Missing player reference or enemy prefabs!");
            return;
        }

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector3 spawnPoint = GetRandomNavMeshPoint(player.position, spawnRadius);

            // Skip if we couldn't find a NavMesh point
            if (Vector3.Distance(spawnPoint, player.position) < 0.1f)
                continue;

            float distanceToPlayer = Vector3.Distance(spawnPoint, player.position);

            // Check minimum distance
            if (distanceToPlayer < minDistanceFromPlayer)
            {
                if (enableDebug)
                {
                    Debug.Log($"Attempt {i}: Too close ({distanceToPlayer:F1}m < {minDistanceFromPlayer}m)");
                    Debug.DrawRay(spawnPoint, Vector3.up * 3f, Color.blue, debugDrawDuration);
                }
                continue;
            }

            // Check line of sight
            Vector3 dir = (player.position - spawnPoint).normalized;
            Vector3 rayOrigin = spawnPoint + Vector3.up * 0.5f;

            if (enableDebug)
                Debug.DrawRay(rayOrigin, dir * distanceToPlayer, Color.red, debugDrawDuration);

            // Spawn if there's NO direct line of sight (raycast doesn't hit anything)
            if (!Physics.Raycast(rayOrigin, dir, distanceToPlayer, visionBlockMask))
            {
                // Spawn the enemy
                GameObject prefab = enemyFabs[Random.Range(0, enemyFabs.Length)];
                GameObject newEnemy = Instantiate(prefab, spawnPoint, Quaternion.identity);
                currentEnemies++;

                // Subscribe to death event
                Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.OnEnemyDeath += () => currentEnemies--;
                }

                if (enableDebug)
                {
                    Debug.Log($"SUCCESS: Spawned enemy at {distanceToPlayer:F1}m from player");
                    Debug.DrawRay(spawnPoint, Vector3.up * 5f, Color.green, debugDrawDuration * 2);
                }

                return; // Exit after successful spawn
            }

            if (enableDebug)
                Debug.DrawRay(spawnPoint, Vector3.up * 3f, Color.magenta, debugDrawDuration);
        }

        // Failed to spawn after all attempts
        if (enableDebug)
            Debug.LogWarning($"Failed to spawn after {maxSpawnAttempts} attempts");
    }

    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * radius;
        randomPos.y = center.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return center;
    }

    private void ValidateSetup()
    {
        if (!enableDebug) return;

        Debug.Log("=== ENEMY SPAWNER INITIALIZED ===");
        Debug.Log($"Enemy Prefabs: {enemyFabs?.Length ?? 0}");
        Debug.Log($"Max Enemies: {maxEnemis}");
        Debug.Log($"Spawn Radius: {spawnRadius}m");
        Debug.Log($"Min Distance: {minDistanceFromPlayer}m");
        Debug.Log($"Vision Mask: {LayerMaskToString(visionBlockMask)}");

        // Warnings
        if (enemyFabs == null || enemyFabs.Length == 0)
            Debug.LogError("No enemy prefabs assigned!");

        if (player == null)
            Debug.LogError("Player transform not assigned!");

        if (visionBlockMask.value == 0)
            Debug.LogWarning("Vision block mask is empty!");

        // Check NavMesh
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
            Debug.LogWarning("No NavMesh found! Did you bake it?");
    }

    private string LayerMaskToString(LayerMask mask)
    {
        if (mask.value == 0) return "None";

        string result = "";
        for (int i = 0; i < 32; i++)
        {
            if ((mask.value & (1 << i)) != 0)
            {
                string layerName = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(layerName))
                {
                    if (result.Length > 0) result += ", ";
                    result += layerName;
                }
            }
        }
        return string.IsNullOrEmpty(result) ? "Unnamed" : result;
    }

    // Visual helpers in Scene view
    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        // Spawn radius (yellow)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.position, spawnRadius);

        // Minimum distance (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, minDistanceFromPlayer);

        // Label
#if UNITY_EDITOR
        UnityEditor.Handles.Label(player.position + Vector3.up * spawnRadius, "Spawn Area");
        UnityEditor.Handles.Label(player.position + Vector3.up * minDistanceFromPlayer, "Min Distance");
#endif
    }
}