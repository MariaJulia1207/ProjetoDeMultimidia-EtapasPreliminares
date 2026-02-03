using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 8f;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    [Header("Gravidade")]
    public float gravidadeNormal = 3f;
    public float gravidadePuloNoAr = 5f;

    [Header("Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDisparo;

    [Header("Colisão")]
    public BoxCollider2D colliderEmPe;
    public BoxCollider2D colliderAbaixado;

    [Header("Vida")]
    public int vidaMaxima = 5;
    private int vidaAtual;

    [Header("HUD")]
    [SerializeField] private LifeHUD lifeHUD;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("Áudio")]
    public AudioClip somTiro;
    public AudioClip[] sonsPasso;
    public AudioClip somImpactoMorte;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audio;

    private bool estaNoChao;
    private bool puloNoArDisponivel;
    private bool abaixado;
    private bool morto;

    private Color corOriginal;

    // ================= START =================
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        rb.gravityScale = gravidadeNormal;

        colliderEmPe.enabled = true;
        colliderAbaixado.enabled = false;

        vidaAtual = vidaMaxima;
        lifeHUD.AtualizarVidas(vidaAtual);

        corOriginal = sprite.color;
    }

    // ================= UPDATE =================
    void Update()
    {
        if (morto) return;

        Movimento();
        Abaixar();
        AtualizarCoyoteTime();
        Pulo();
        Atirar();
    }

    // ================= MOVIMENTO =================
    void Movimento()
    {
        float h = 0f;

        if (!abaixado)
        {
            if (Input.GetKey(KeyCode.RightArrow)) h = 1;
            else if (Input.GetKey(KeyCode.LeftArrow)) h = -1;
        }

        rb.linearVelocity = new Vector2(h * velocidade, rb.linearVelocity.y);
        anim.SetFloat("speed", Mathf.Abs(h));

        if (h != 0)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
    }

    // ================= ABAIXAR =================
    void Abaixar()
    {
        abaixado = Input.GetKey(KeyCode.DownArrow);
        anim.SetBool("isCrouching", abaixado);

        colliderEmPe.enabled = !abaixado;
        colliderAbaixado.enabled = abaixado;
    }

    // ================= COYOTE TIME =================
    void AtualizarCoyoteTime()
    {
        if (estaNoChao)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    // ================= PULO =================
    void Pulo()
    {
        if (abaixado) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (estaNoChao || coyoteTimeCounter > 0f)
            {
                ExecutarPulo(gravidadeNormal);
                puloNoArDisponivel = true;
                coyoteTimeCounter = 0f;
            }
            else if (puloNoArDisponivel)
            {
                ExecutarPulo(gravidadePuloNoAr);
                puloNoArDisponivel = false;
            }
        }
    }

    void ExecutarPulo(float gravidade)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        rb.gravityScale = gravidade;

        estaNoChao = false;
        anim.SetBool("isGrounded", false);
        anim.SetTrigger("jump");
    }

    // ================= ATAQUE =================
    void Atirar()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("shoot");

            GameObject proj = Instantiate(
                projetilPrefab,
                pontoDisparo.position,
                Quaternion.identity
            );

            float direcao = transform.localScale.x;
            proj.GetComponent<Projetil>().DefinirDirecao(direcao);

            if (somTiro != null)
                audio.PlayOneShot(somTiro);
        }
    }

    // ================= CHÃO =================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            estaNoChao = true;
            puloNoArDisponivel = false;
            rb.gravityScale = gravidadeNormal;
            anim.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            estaNoChao = false;
            anim.SetBool("isGrounded", false);
        }
    }

    // ================= DANO =================
    public void TomarDano(int dano)
    {
        if (morto) return;

        vidaAtual -= dano;
        vidaAtual = Mathf.Max(vidaAtual, 0);
        lifeHUD.AtualizarVidas(vidaAtual);

        StartCoroutine(FlashVermelho());

        if (vidaAtual <= 0)
            IniciarMorte();
    }

    // ================= FLASH VERMELHO =================
    IEnumerator FlashVermelho()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = corOriginal;
    }

    // ================= MORTE =================
    void IniciarMorte()
    {
        morto = true;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        anim.SetTrigger("death");
    }

    // 🔔 CHAMADO POR ANIMATION EVENT NO FINAL DO CLIP "Death"
    public void FinalizarMorte()
    {
        if (somImpactoMorte != null)
            audio.PlayOneShot(somImpactoMorte);

        GameOverManager.Instance.MostrarGameOver();
    }

    // ================= PASSOS =================
    public void TocarPasso()
    {
        if (!estaNoChao || abaixado || sonsPasso.Length == 0) return;

        audio.pitch = Random.Range(0.95f, 1.05f);
        audio.PlayOneShot(sonsPasso[Random.Range(0, sonsPasso.Length)]);
    }
    
    public void TocarTiro()
{
    if (audio != null && somTiro != null)
    {
        audio.PlayOneShot(somTiro);
    }
}

}
