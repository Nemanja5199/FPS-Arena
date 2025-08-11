using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    

    [Header("Settings")]
    [SerializeField] private float delayBeforeLoad = 0.2f; 

    void Start()
    {
     
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
 
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

        // Load menu scene
        SceneManager.LoadScene("MainMenu"); 
    }

    private void PlayClickSound()
    {
        if (AudioManager.Instance != null && AudioManager.Instance.clickSound != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.clickSound);
        }
    }

  
}