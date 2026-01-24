using UnityEngine;

public class MusicArea : MonoBehaviour
{
    public AudioSource musica;
    public float fadeTime = 1.5f;

    private Coroutine fadeRoutine;

    void Start()
    {
        musica.volume = 0f;
        musica.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);

            musica.Play();
            fadeRoutine = StartCoroutine(FadeMusica(1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);

            fadeRoutine = StartCoroutine(FadeMusica(0f));
        }
    }

    System.Collections.IEnumerator FadeMusica(float alvo)
    {
        float volumeInicial = musica.volume;
        float tempo = 0f;

        while (tempo < fadeTime)
        {
            tempo += Time.deltaTime;
            musica.volume = Mathf.Lerp(volumeInicial, alvo, tempo / fadeTime);
            yield return null;
        }

        musica.volume = alvo;

        if (alvo == 0f)
            musica.Stop();
    }
}
