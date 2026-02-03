using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Dano")]
    public int dano = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TomarDano(dano);
        }
    }
}
