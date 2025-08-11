using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    [Header("Music Playlist")]
    public List<AudioClip> playlist = new List<AudioClip>();
    public bool shufflePlaylist = false;
    public bool loopPlaylist = true;

    private int currentTrackIndex = 0;
    private List<int> shuffledIndices;

    [Header("Sound Effects")]
    public AudioClip clickSound;
    public AudioClip gunshotSound;
    public AudioClip reloadSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {


 
        if (playlist.Count > 0)
        {
            if (shufflePlaylist)
            {
                ShufflePlaylist();
            }
            PlayNextTrack();
        }
    }

    void Update()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

        if (playlist.Count > 0 && !musicSource.isPlaying && musicSource.time == 0)
        {
            PlayNextTrack();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = false; 
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }


    public void PlayNextTrack()
    {
        if (playlist.Count == 0) return;

        if (shufflePlaylist && shuffledIndices != null)
        {
            PlayMusic(playlist[shuffledIndices[currentTrackIndex]]);
        }
        else
        {
            PlayMusic(playlist[currentTrackIndex]);
        }

        currentTrackIndex++;

    
        if (currentTrackIndex >= playlist.Count)
        {
            if (loopPlaylist)
            {
                currentTrackIndex = 0;
                if (shufflePlaylist)
                {
                    ShufflePlaylist();
                }
            }
            else
            {
                currentTrackIndex = playlist.Count - 1; 
            }
        }
    }


 
    private void ShufflePlaylist()
    {
        shuffledIndices = new List<int>();
        for (int i = 0; i < playlist.Count; i++)
        {
            shuffledIndices.Add(i);
        }

     
        for (int i = shuffledIndices.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = shuffledIndices[i];
            shuffledIndices[i] = shuffledIndices[randomIndex];
            shuffledIndices[randomIndex] = temp;
        }
    }

    public void ToggleShuffle()
    {
        shufflePlaylist = !shufflePlaylist;
        if (shufflePlaylist)
        {
            ShufflePlaylist();
        }
    }

 




   
}