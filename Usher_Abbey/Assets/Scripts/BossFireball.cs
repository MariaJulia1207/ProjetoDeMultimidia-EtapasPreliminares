using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float velocidade = 6f;
    public int dano = 1;

    private float direcao;

    public void DefinirDirecao(float dir)
    {
        direcao = dir;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TomarDano(dano);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
