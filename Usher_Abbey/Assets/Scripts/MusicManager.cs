using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource musicaAtual;
    private int prioridadeAtual = -1;

    void Awake()
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

    public void TocarMusica(AudioSource novaMusica, int prioridade)
    {
        if (novaMusica == null) return;

        // Não troca se a música atual tiver prioridade maior
        if (prioridade < prioridadeAtual) return;

        if (musicaAtual == novaMusica) return;

        if (musicaAtual != null)
            musicaAtual.Stop();

        musicaAtual = novaMusica;
        prioridadeAtual = prioridade;

        musicaAtual.Play();
    }

    public void PararMusica(AudioSource musica, int prioridade)
    {
        if (musicaAtual != musica) return;
        if (prioridade < prioridadeAtual) return;

        musicaAtual.Stop();
        musicaAtual = null;
        prioridadeAtual = -1;
    }
}
