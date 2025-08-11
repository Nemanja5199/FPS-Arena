using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Volume Text Display (Optional)")]
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    [Header("Mouse Sensitivity")]
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    [Header("Settings Range")]
    [SerializeField] private float minSensitivity = 0.5f;
    [SerializeField] private float maxSensitivity = 5f;
    [SerializeField] private static  float defaultSensitivity = 1f;

    void Start()
    {
        LoadSettings();

        // Add listeners to sliders
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.minValue = minSensitivity;
            mouseSensitivitySlider.maxValue = maxSensitivity;
            mouseSensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        }
    }

    void LoadSettings()
    {
        // Load saved settings or use defaults
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);

  
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = savedMusicVolume;
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = savedSFXVolume;
        }

        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.value = savedSensitivity;
        }


        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.musicVolume = savedMusicVolume;
            AudioManager.Instance.sfxVolume = savedSFXVolume;
        }

        UpdateVolumeTexts();
        UpdateSensitivityText();
    }

    void OnMusicVolumeChanged(float value)
    {
      
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.musicVolume = value;
        }

      
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

    
        if (musicVolumeText != null)
        {
            musicVolumeText.text = $"{Mathf.RoundToInt(value * 100)}%";
        }
    }

    void OnSFXVolumeChanged(float value)
    {
      
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.sfxVolume = value;

  
        }

      
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();

        if (sfxVolumeText != null)
        {
            sfxVolumeText.text = $"{Mathf.RoundToInt(value * 100)}%";
        }
    }

    void OnSensitivityChanged(float value)
    {
      
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();

        if (sensitivityText != null)
        {
            sensitivityText.text = value.ToString("F1");
        }
    }

    void UpdateVolumeTexts()
    {
        if (musicVolumeText != null && musicVolumeSlider != null)
        {
            musicVolumeText.text = $"{Mathf.RoundToInt(musicVolumeSlider.value * 100)}%";
        }

        if (sfxVolumeText != null && sfxVolumeSlider != null)
        {
            sfxVolumeText.text = $"{Mathf.RoundToInt(sfxVolumeSlider.value * 100)}%";
        }
    }

    void UpdateSensitivityText()
    {
        if (sensitivityText != null && mouseSensitivitySlider != null)
        {
            sensitivityText.text = mouseSensitivitySlider.value.ToString("F1");
        }
    }

 


    public static float GetMouseSensitivity()
    {
        return PlayerPrefs.GetFloat("MouseSensitivity", defaultSensitivity);
    }
}