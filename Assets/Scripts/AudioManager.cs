using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("Music")]
    public AudioSource MusicSource;
    [SerializeField] private AudioClip backgroundMusic;

    [Header("SFX")]
    public AudioSource SFXSource;
    [SerializeField] private AudioClip coinPickup;
    [SerializeField] private AudioClip playerHit;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic();
        }
    }

    private void PlayBackgroundMusic()
    {
        MusicSource.clip = backgroundMusic;
        MusicSource.loop = true;
        MusicSource.Play();
    }
}
  
