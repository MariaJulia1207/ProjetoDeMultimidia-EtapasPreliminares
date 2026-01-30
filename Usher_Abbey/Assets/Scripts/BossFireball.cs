using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float velocidade = 6f;
    public int dano = 1;

    [Header("Áudio")]
    public AudioClip somExplosaoPequena;
    public AudioClip somChiado;

    private float direcao = 1f;

    public void DefinirDirecao(float dir)
    {
        direcao = Mathf.Sign(dir);
        transform.localScale = new Vector3(direcao, 1, 1);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidade * direcao * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TocarSom(somExplosaoPequena);
            TocarSom(somChiado);

            collision.GetComponentInParent<PlayerController>()?.TomarDano(dano);
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void TocarSom(AudioClip clip)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}

/**/