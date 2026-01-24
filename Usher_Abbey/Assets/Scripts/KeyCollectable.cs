using UnityEngine;

public class KeyCollectable : MonoBehaviour, ICollectable
{
    public AudioClip somColeta;

    public void Collect()
    {
        PlayerKey playerKey = FindObjectOfType<PlayerKey>();

        if (playerKey != null)
            playerKey.ColetarChave();

        if (somColeta != null)
            AudioSource.PlayClipAtPoint(somColeta, transform.position);

        Destroy(gameObject);
    }
}

