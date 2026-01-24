using UnityEngine;

public class BossFireball : MonoBehaviour
{
    public float velocidade = 6f;
    public int dano = 1;
    public float tempoVida = 4f;

    private Vector2 direcao;

    void Start()
    {
        Destroy(gameObject, tempoVida);
    }

    public void DefinirDirecao(Vector2 dir)
    {
        direcao = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()
                .TomarDano(dano);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
