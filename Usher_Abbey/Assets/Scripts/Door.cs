using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioClip somAbrirPorta;

    private Animator anim;
    private Collider2D colliderFisico;
    private bool aberta;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        colliderFisico = GetComponentInParent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aberta) return;

        if (collision.CompareTag("Player"))
        {
            PlayerKey playerKey = collision.GetComponent<PlayerKey>();
Debug.Log("Colidiu");
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
    }

    // chamado pelo Animation Event
    public void DesativarCollider()
    {
        colliderFisico.enabled = false;
    }
}