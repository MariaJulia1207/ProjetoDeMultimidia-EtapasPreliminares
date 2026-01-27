using UnityEngine;

public class LifeCollectable : MonoBehaviour, ICollectable
{
    public int vidaExtra = 1;
    public AudioClip somColeta;

    public void Collect()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null)
            player.TomarDano(-vidaExtra);

        if (somColeta != null)
            AudioSource.PlayClipAtPoint(somColeta, transform.position);

        Destroy(gameObject);
    }
}
