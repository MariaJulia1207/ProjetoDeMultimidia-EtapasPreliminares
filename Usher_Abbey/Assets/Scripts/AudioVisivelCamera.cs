using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisivelNaCamera : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 3D
        audioSource.loop = true;
    }

    void OnBecameVisible()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    void OnBecameInvisible()
    {
        audioSource.Stop();
    }
}
