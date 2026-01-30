using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Audio Clips")]
    public AudioClip enemyHitSound1;
    public AudioClip enemyHitSound2;
    public AudioClip playerJump;
    public AudioClip liftSound;
    public AudioClip backgroundMusic;
    public AudioClip shootAsteroidSound;
    public AudioClip sprintingSound;
  


    private void Awake()
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

    private void Start()
    {
        PlayMusic(backgroundMusic);

        Instance.playAlarmSound(sfxSource);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip != null && musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void playEnemyHitSound(AudioSource Source)
    {
        if (enemyHitSound != null && Source != null)
        {
            Source.PlayOneShot(enemyHitSound);
        }
    }

    public void playPlayerHitSound(AudioSource Source)
    {
        if (playerHitSound != null && Source != null)
        {
            Source.PlayOneShot(playerHitSound);
        }
    }

    public void playPickupSound(AudioSource Source)
    {
        if (pickupSound != null && Source != null)
        {
            Source.PlayOneShot(pickupSound);
        }
    }

    public void playShootSound(AudioSource Source)
    {
        if (shootSound != null && Source != null)
        {
            Source.PlayOneShot(shootSound);
            Source.volume = .3f;
            //Debug.LogWarning("Shootsound!!");
        }
    }

    public void playExplodeSound(AudioSource Source)
    {
        if (explodeSound != null && Source != null)
        {
            Source.PlayOneShot(explodeSound);
        }
    }

    public void playRocketSound(AudioSource Source)
    {
        if (rocketSound != null && Source != null)
        {
            Source.clip = rocketSound;
            Source.playOnAwake = false;
            Source.bypassEffects = true;
            Source.volume = 1f;
            Source.loop = true;
            Source.Play();
        }
    }

    public void playRobotSound(AudioSource Source)
    {
        if (robotSound != null && Source != null)
        {
            Source.clip = robotSound;
            Source.playOnAwake = false;
            Source.bypassEffects = true;
            Source.volume = .01f;
            Source.loop = true;
            Source.Play();
        }
    }

    public void playDoorSound(AudioSource Source)
    {
        if (doorSound != null && Source != null)
        {
            Source.PlayOneShot(doorSound);
        }
    }

    public void playScanSound(AudioSource Source)
    {
        if (scanSound != null && Source != null)
        {
            Source.PlayOneShot(scanSound);
        }
    }

    public void playWrongSound(AudioSource Source)
    {
        if (wrongSound != null && Source != null)
        {
            Source.PlayOneShot(wrongSound);
        }
    }
    public void playAlarmSound(AudioSource Source)
    {
        if (alarmSound != null && Source != null)
        {
            Source.PlayOneShot(alarmSound);
        }
    }
}