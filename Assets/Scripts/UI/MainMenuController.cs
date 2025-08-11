using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backButtonHowToPlay;
    [SerializeField] private Button backButtonSettings;

    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject settingsPanel;


    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playButton.onClick.AddListener(OnPlayClicked);
        howToPlayButton.onClick.AddListener(OnHowToPlayClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);

        if (backButtonHowToPlay != null)
        {
            backButtonHowToPlay.onClick.AddListener(OnBackClicked);
        }

        if(backButtonSettings != null)
        {
            backButtonSettings.onClick.AddListener (OnBackClicked);
        }

 
        ShowMainMenu();
    }

    public void OnPlayClicked()
    {
        PlayClickSound();

      
        StartCoroutine(WaitForSoundThenLoad(0.2f)); 
    }

    public void OnHowToPlayClicked()
    {
        PlayClickSound();
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void OnBackClicked()
    {
        PlayClickSound();
        ShowMainMenu();
    }

    public void OnSettingsClicked()
    {
        PlayClickSound();
        ShowSettingsMenu();
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void ShowSettingsMenu()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    private void PlayClickSound()
    {

        if (AudioManager.Instance != null && AudioManager.Instance.clickSound != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.clickSound);
        }
      
      
    }

    private IEnumerator WaitForSoundThenLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}