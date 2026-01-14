using UnityEngine;

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

    private Rigidbody2D rb;
    private Animator anim;

    private bool estaNoChao;
    private bool puloNoArDisponivel;
    private bool abaixado;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    rb.gravityScale = gravidadeNormal;
    
    colliderEmPe.enabled = true;
    colliderAbaixado.enabled = false;
    vidaAtual = vidaMaxima;
}


    void Update()
    {
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
            if (Input.GetKey(KeyCode.RightArrow))
                h = 1;
            else if (Input.GetKey(KeyCode.LeftArrow))
                h = -1;
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
        if (abaixado) return; // bloqueia pulo abaixado

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pulo normal (chão ou coyote time)
            if (estaNoChao || coyoteTimeCounter > 0f)
            {
                ExecutarPulo(gravidadeNormal);
                puloNoArDisponivel = true;
                coyoteTimeCounter = 0f;
            }
            // Pulo no ar (apenas uma vez)
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
            Instantiate(projetilPrefab, pontoDisparo.position, pontoDisparo.rotation);
        }
    }

    // ================= COLISÃO COM CHÃO =================
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
    vidaAtual -= dano;
    anim.SetTrigger("hurt");

    if (vidaAtual <= 0)
        Morrer();
}
    // ================= MORRER =================
void Morrer()
{
    anim.SetTrigger("death");
    rb.linearVelocity = Vector2.zero;
    this.enabled = false;
}
    // ================= DANO INIMIGO =================
private void OnCollisionEnter2DEnemy(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        TomarDano(1);
    }
}

}
