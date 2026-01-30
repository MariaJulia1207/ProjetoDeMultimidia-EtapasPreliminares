using UnityEngine;

public class MusicArea : MonoBehaviour
{
    public AudioSource musica;
    public int prioridade = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        MusicManager.Instance.TocarMusica(musica, prioridade);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        MusicManager.Instance.PararMusica(musica, prioridade);
    }
}
