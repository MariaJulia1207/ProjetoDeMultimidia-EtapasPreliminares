using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 2f;
    public int vida = 3;
    public int danoContato = 1;

    public AudioSource audioSource;
    public AudioClip somPasso;
    public AudioClip somMorte;
    public AudioClip somDano;

    private Transform alvoAtual;
    private Animator anim;
    private bool morto;

    void Start()
    {
        anim = GetComponent<Animator>();
        alvoAtual = pontoB;
    }

    void Update()
    {
        if (morto) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            alvoAtual.position,
            velocidade * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, alvoAtual.position) < 0.1f)
{
    if (alvoAtual == pontoA)
        alvoAtual = pontoB;
    else
        alvoAtual = pontoA;

    float direcao = Mathf.Sign(alvoAtual.position.x - transform.position.x);
    transform.localScale = new Vector3(direcao, 1, 1);
}

    }

    public void TomarDano(int dano)
    {
        if (morto) return;

        vida -= dano;
        anim.SetTrigger("hurt");
        audioSource.PlayOneShot(somDano);
        GetComponent<DamageFlash>()?.Flash();


        if (vida <= 0)
            Morrer();
    }

    void Morrer()
    {
        morto = true;
        anim.SetTrigger("death");
        audioSource.PlayOneShot(somMorte);
        GetComponent<Collider2D>().enabled = false;
    }

    // Animation Event
    public void Destruir()
    {
        Destroy(gameObject);
    }

    // 🔥 DANO POR CONTATO
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject
                .GetComponent<PlayerController>()
                ?.TomarDano(danoContato);
    }
}
