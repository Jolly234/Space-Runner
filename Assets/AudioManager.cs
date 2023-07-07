using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private int currentClipIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private void Start()
    {
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }
}


    // Add methods to control audio playback, volume, etc.

