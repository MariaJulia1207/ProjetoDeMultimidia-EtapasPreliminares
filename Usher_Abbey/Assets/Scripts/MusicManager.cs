using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Configuração")]
    public float fadeDuration = 1f;

    private AudioSource musicaAtual;
    private int prioridadeAtual = -1;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ▶️ Pedir para tocar uma música
    public void PlayMusic(AudioSource source, int prioridade)
    {
        if (source == null) return;

        if (prioridade < prioridadeAtual && musicaAtual != source)
            return;

        prioridadeAtual = prioridade;

        if (musicaAtual == source)
            return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(Transicao(source));
    }

    // ⏹️ Pedir para parar uma música
    public void StopMusic(AudioSource source, int prioridade)
    {
        if (prioridade != prioridadeAtual) return;
        if (musicaAtual != source) return;

        prioridadeAtual = -1;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOut(musicaAtual));
    }

    IEnumerator Transicao(AudioSource nova)
    {
        if (musicaAtual != null)
            yield return FadeOut(musicaAtual);

        musicaAtual = nova;

        musicaAtual.volume = 0f;
        musicaAtual.Play();

        yield return FadeIn(musicaAtual);
    }

    IEnumerator FadeIn(AudioSource source)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        source.volume = 1f;
    }

    IEnumerator FadeOut(AudioSource source)
    {
        float start = source.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(start, 0f, t / fadeDuration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
    }
}
