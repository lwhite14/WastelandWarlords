using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance = null;

    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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