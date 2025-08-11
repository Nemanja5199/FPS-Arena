using UnityEngine;

public class Stats : MonoBehaviour
{

    public static Stats Instance;

    [Header("Stats")]
    public float timeSurvived = 0f;

    private float gameStartTime;
    private bool gameActive = true;

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

    void Start()
    {
        gameStartTime = Time.time;
        ResetStats();
    }

    void Update()
    {
        if (gameActive)
        {
            timeSurvived = Time.time - gameStartTime;
        }
    }




    public void OnPlayerDeath()
    {
        gameActive = false;
    }

 
    public string GetTimeString()
    {
        int minutes = Mathf.FloorToInt(timeSurvived / 60);
        int seconds = Mathf.FloorToInt(timeSurvived % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetStats()
    {
        timeSurvived = 0f;
        gameStartTime = Time.time;
        gameActive = true;
    }
}
