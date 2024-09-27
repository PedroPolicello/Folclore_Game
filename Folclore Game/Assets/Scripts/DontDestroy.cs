using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void StopMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }
}
