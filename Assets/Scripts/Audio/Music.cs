using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance = null;

    AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}