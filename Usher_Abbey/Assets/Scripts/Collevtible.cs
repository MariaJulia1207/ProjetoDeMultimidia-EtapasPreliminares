using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Áudio")]
    public AudioClip somColeta;

    [Header("Configurações")]
    public float volume = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Toca o som no mundo (não depende do AudioSource do player)
        AudioSource.PlayClipAtPoint(somColeta, transform.position, volume);

        // Aqui você pode adicionar pontos, vida, etc.
        // Ex: other.GetComponent<PlayerController>().AdicionarPontos(1);

        Destroy(gameObject);
    }
}
