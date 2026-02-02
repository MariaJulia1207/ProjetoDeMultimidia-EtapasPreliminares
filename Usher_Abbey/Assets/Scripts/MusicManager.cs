using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource musicaAtual;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Tocar(AudioSource musica)
    {
        if (musicaAtual == musica) return;

        if (musicaAtual != null)
            musicaAtual.Stop();

        musicaAtual = musica;
        musicaAtual.Play();
    }

    public void Parar(AudioSource musica)
    {
        if (musicaAtual == musica)
        {
            musicaAtual.Stop();
            musicaAtual = null;
        }
    }
}
