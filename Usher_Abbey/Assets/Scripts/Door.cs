using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioClip somAbrirPorta;

    private Animator anim;
    private bool aberta;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aberta) return;

        if (collision.CompareTag("Player"))
        {
            PlayerKey playerKey = collision.GetComponent<PlayerKey>();

            if (playerKey != null && playerKey.temChave)
            {
                AbrirPorta();
                playerKey.UsarChave();
            }
        }
    }

    void AbrirPorta()
    {
        aberta = true;

        anim.SetTrigger("open");

        if (somAbrirPorta != null)
            AudioSource.PlayClipAtPoint(somAbrirPorta, transform.position);

        GetComponent<Collider2D>().enabled = false;
    }

    public void DesativarCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

}
