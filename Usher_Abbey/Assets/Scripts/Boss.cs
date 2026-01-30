using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 2f;
    public Transform pontoDestino;

    private Vector3 pontoInicial;
    private Transform alvoAtual;

    [Header("Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDisparo;
    public float intervaloTiro = 2f;
    public float distanciaAtaque = 6f;

    private float tempoProximoTiro;

    [Header("Vida")]
    public int vidaMaxima = 10;
    private int vidaAtual;
    public bool EstaMorto { get; private set; }

    [Header("Áudio")]
    public AudioClip[] sonsPasso;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        pontoInicial = transform.position;
        alvoAtual = pontoDestino;

        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if (EstaMorto) return;

        Movimentar();
        DetectarJogador();
    }

    // ================= MOVIMENTO =================
    void Movimentar()
    {
        Vector3 direcao = (alvoAtual.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direcao.x * velocidade, rb.linearVelocity.y);

        anim.SetBool("isWalking", Mathf.Abs(direcao.x) > 0.1f);

        if (direcao.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direcao.x), 1, 1);

        if (Vector2.Distance(transform.position, alvoAtual.position) < 0.2f)
        {
            alvoAtual = alvoAtual == pontoDestino
                ? CriarTransformTemporario(pontoInicial)
                : pontoDestino;
        }
    }

    Transform CriarTransformTemporario(Vector3 posicao)
    {
        GameObject temp = new GameObject("PontoInicialBoss");
        temp.transform.position = posicao;
        return temp.transform;
    }

    // ================= ATAQUE =================
    void DetectarJogador()
    {
        if (Time.time < tempoProximoTiro) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.transform.position);
        if (distancia > distanciaAtaque) return;

        float direcaoPlayer = Mathf.Sign(player.transform.position.x - transform.position.x);

        // Só atira se o jogador estiver à frente
        if (direcaoPlayer == Mathf.Sign(transform.localScale.x))
            Atacar(direcaoPlayer);
    }

    void Atacar(float direcao)
    {
        tempoProximoTiro = Time.time + intervaloTiro;

        anim.SetTrigger("attack");

        GameObject proj = Instantiate(
            projetilPrefab,
            pontoDisparo.position,
            Quaternion.identity
        );

        proj.GetComponent<BossProjectile>().DefinirDirecao(direcao);
    }

    // ================= DANO =================
    public void TomarDano(int dano)
    {
        if (EstaMorto) return;

        vidaAtual -= dano;

        if (vidaAtual <= 0)
            Morrer();
    }

    // ================= MORTE =================
    void Morrer()
    {
        EstaMorto = true;

        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("death");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    // ================= PASSOS (Animation Event) =================
    public void TocarPasso()
    {
        if (sonsPasso.Length == 0) return;

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(sonsPasso[Random.Range(0, sonsPasso.Length)]);
    }
}
