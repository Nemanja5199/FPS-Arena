using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip singleTrack; 
    [SerializeField] private bool usePlaylist = false;  

    void Start()
    {
        if (AudioManager.Instance == null) return;

        if (usePlaylist)
        {
            
            AudioManager.Instance.PlayNextTrack();
        }
        else if (singleTrack != null)
        {
           
            AudioManager.Instance.PlayMusic(singleTrack);
        }
    }
}