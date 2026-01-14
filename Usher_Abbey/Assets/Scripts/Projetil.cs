using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    public int dano = 10;
    public float tempoVida = 3f;

    void Start()
    {
        Destroy(gameObject, tempoVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReceberDano(dano);
            }

            Destroy(gameObject);
        }
    }
}
