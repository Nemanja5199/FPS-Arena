using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    private AudioSource clickAudioSource;

    void Start()
    {
        // Get the AudioSource from the playButton GameObject
        clickAudioSource = playButton.GetComponent<AudioSource>();

        playButton.onClick.AddListener(OnPlayClicked);
    }

    public void OnPlayClicked()
    {
        if (clickAudioSource != null)
        {
            clickAudioSource.Play();
            StartCoroutine(WaitForSoundThenLoad(clickAudioSource.clip.length));
        }
        else
        {
            LoadGameScene();
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
