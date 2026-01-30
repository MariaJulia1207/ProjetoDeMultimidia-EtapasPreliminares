using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player =
                collision.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                Debug.Log("Espinhos tocados por: " + collision.name);

                player.MorrerInstantaneamente();
            }
        }
    }
}
