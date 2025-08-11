using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;


  /*  [Header("Optional Stats Display")]
    [SerializeField] private TMPro.TextMeshProUGUI survivalTimeText;
    [SerializeField] private TMPro.TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;*/

    [Header("Settings")]
    [SerializeField] private float delayBeforeLoad = 0.2f; 

    void Start()
    {
        // Add button listeners
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);

        
    }

    void OnEnable()
    {
      /*  
        DisplayGameStats();*/
    }

    public void OnPlayAgainClicked()
    {
        PlayClickSound();
        StartCoroutine(RestartGame());
    }

    public void OnMainMenuClicked()
    {
        PlayClickSound();
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator RestartGame()
    {

        yield return new WaitForSecondsRealtime(delayBeforeLoad);

   
        Time.timeScale = 1f;

     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadMainMenu()
    {
    
        yield return new WaitForSecondsRealtime(delayBeforeLoad);

    
        Time.timeScale = 1f;

 
        SceneManager.LoadScene("MainMenu"); 
    }

    private void PlayClickSound()
    {
        if (AudioManager.Instance != null && AudioManager.Instance.clickSound != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.clickSound);
        }
    }

    private void DisplayGameStats()
    {
        // Optional: Display game stats if you're tracking them
        // You'd get these values from your game manager or player stats

        /*
        if (survivalTimeText != null)
        {
            float survivalTime = Time.timeSinceLevelLoad;
            survivalTimeText.text = $"Survived: {survivalTime:F1}s";
        }
        
        if (enemiesKilledText != null)
        {
            // Get from your game stats
            int enemiesKilled = GameStats.Instance.enemiesKilled;
            enemiesKilledText.text = $"Enemies Killed: {enemiesKilled}";
        }
        
        if (scoreText != null)
        {
            // Get from your game stats
            int score = GameStats.Instance.score;
            scoreText.text = $"Score: {score}";
        }
        */
    }
}