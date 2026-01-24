using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade = 2f;
    public int dano = 1;

    public Transform pontoChao;
    public LayerMask layerChao;

    private Rigidbody2D rb;
    private bool indoDireita = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(
            (indoDireita ? 1 : -1) * velocidade,
            rb.linearVelocity.y
        );

        bool temChao = Physics2D.Raycast(
            pontoChao.position,
            Vector2.down,
            0.2f,
            layerChao
        );

        if (!temChao)
            Virar();
    }

    void Virar()
    {
        indoDireita = !indoDireita;
        transform.localScale = new Vector3(
            -transform.localScale.x,
            1,
            1
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject
                .GetComponent<PlayerController>()
                .TomarDano(dano);
        }
    }
}
