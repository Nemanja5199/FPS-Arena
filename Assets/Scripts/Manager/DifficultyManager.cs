using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    [Header("Current Stats")]
    [SerializeField] 
    private int totalKills = 0;
    [SerializeField] 
    private int difficultyLevel = 1;

    [Header("Difficulty Settings")]
    [SerializeField] 
    private int killsPerLevel = 5; 
    [SerializeField] 
    private int maxDifficultyLevel = 10; 

    [Header("Enemy Damage Scaling")]
    [SerializeField] 
    private float damageMultiplier = 1f;
    [SerializeField] 
    private float damageIncreasePerLevel = 0.15f;
    [SerializeField] 
    private float maxDamageMultiplier = 2.5f;

    [Header("Spawn Rate Scaling")]
    [SerializeField] 
    private float spawnRateMultiplier = 1f;
    [SerializeField] 
    private float spawnRateIncreasePerLevel = 0.15f; 
    [SerializeField] 
    private int extraEnemiesPerLevel = 1; 

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnEnemyKilled()
    {
        totalKills++;
        Debug.Log($"Total Kills: {totalKills}");

     
        int newLevel = (totalKills / killsPerLevel) + 1;

     
        newLevel = Mathf.Min(newLevel, maxDifficultyLevel);

        if (newLevel > difficultyLevel)
        {
            difficultyLevel = newLevel;
            UpdateDifficulty();
        }
    }

    private void UpdateDifficulty()
    {
  
        damageMultiplier = 1f + (damageIncreasePerLevel * (difficultyLevel - 1));
        damageMultiplier = Mathf.Min(damageMultiplier, maxDamageMultiplier);

  
        spawnRateMultiplier = 1f + (spawnRateIncreasePerLevel * (difficultyLevel - 1));

        Debug.LogWarning($"DIFFICULTY LEVEL {difficultyLevel}!");
        Debug.Log($"Enemy Damage: {damageMultiplier:F1}x | Spawn Rate: {spawnRateMultiplier:F1}x faster");


        if (difficultyLevel >= maxDifficultyLevel)
        {
            Debug.LogWarning("MAX DIFFICULTY REACHED!");
        }
    }


    public float GetDamageMultiplier() => damageMultiplier;
    public float GetSpawnRateMultiplier() => spawnRateMultiplier;
    public int GetExtraEnemies() => extraEnemiesPerLevel * (difficultyLevel - 1);
    public int GetDifficultyLevel() => difficultyLevel;
    public int GetTotalKills() => totalKills;
    public bool IsMaxDifficulty() => difficultyLevel >= maxDifficultyLevel;
}