using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somAbrir;

    private Animator anim;
    private bool aberta;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (aberta) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            aberta = true;
            anim.SetTrigger("open");
            audioSource.PlayOneShot(somAbrir);
            GetComponent<Collider2D>().enabled = false;
        }
    }
    public void DesativarCollider()
{
    Collider2D col = GetComponent<Collider2D>();
    if (col != null)
        col.enabled = false;
}

}
