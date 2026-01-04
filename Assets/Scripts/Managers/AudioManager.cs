using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; 
    public AudioSource sfxSource;   

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip jumpClip;
    public AudioClip checkpointClip;
    public AudioClip deathClip;
    

    void Awake()
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
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.pitch = 1f;
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySFXRandomPitch(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(clip);
        }
    }
}