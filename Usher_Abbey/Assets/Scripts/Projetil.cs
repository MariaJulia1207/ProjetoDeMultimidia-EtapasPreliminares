using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    private float direcao = 1f;

    public void DefinirDirecao(float dir)
    {
        direcao = dir;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

