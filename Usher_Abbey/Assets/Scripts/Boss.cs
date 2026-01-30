using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 2f;
    public float distanciaMinima = 4f;

    [Header("Vida")]
    public int vidaMaxima = 20;
    private int vidaAtual;

    [Header("Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDisparo;
    public float tempoEntreAtaques = 2f;

    private float tempoAtaque;
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private bool morto;
    [Header("Música do Boss")]
    public AudioSource musicaBoss;
    public int prioridadeMusicaBoss = 2;


void OnEnable()
{
    MusicManager.Instance.PlayMusic(musicaBoss, 2);

}

void OnDestroy()
    {
        MusicManager.Instance.StopMusic(musicaBoss, prioridadeMusicaBoss);
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        vidaAtual = vidaMaxima;
        MusicManager.Instance.PlayMusic(musicaBoss, prioridadeMusicaBoss);
    }

    void Update()
    {
        if (morto) return;

        SeguirJogador();
        Atacar();
    }

    void SeguirJogador()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia > distanciaMinima)
        {
            Vector2 direcao = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direcao.x * velocidade, 0);
            anim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

            transform.localScale = new Vector3(
                Mathf.Sign(direcao.x), 1, 1
            );
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetFloat("speed", 0);
        }
    }

    void Atacar()
    {
        tempoAtaque += Time.deltaTime;

        if (tempoAtaque >= tempoEntreAtaques)
        {
            tempoAtaque = 0;
            anim.SetTrigger("attack");
        }
    }

    // Chamado por EVENTO de animação
    public void DispararProjetil()
    {
        float direcao = Mathf.Sign(transform.localScale.x);

        GameObject proj = Instantiate(
            projetilPrefab,
            pontoDisparo.position,
            Quaternion.identity
        );

        proj.GetComponent<BossProjectile>().DefinirDirecao(direcao);
    }

    public void TomarDano(int dano)
    {
        if (morto) return;

        vidaAtual -= dano;

        if (vidaAtual <= 0)
            Morrer();
    }

    void Morrer()
    {
        morto = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject
                .GetComponent<PlayerController>()
                .TomarDano(1);
        }
    }
}
