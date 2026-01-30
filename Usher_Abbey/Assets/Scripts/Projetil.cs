using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    public int dano = 1;

    [Header("Áudio")]
    public AudioClip somExplosaoPequena;
    public AudioClip somExplosao;
    public AudioClip somChiadoAgudo;
    public AudioClip somChiadoGrave;

    private AudioSource audioSource;
    private float direcao = 1f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        // ===== SUPERFÍCIE SÓLIDA =====
        if (collision.CompareTag("Ground"))
        {
            TocarSom(somExplosaoPequena);
            Destroy(gameObject);
            return;
        }

        // ===== INIMIGO COMUM =====
        if (collision.CompareTag("Enemy"))
        {
            TocarSom(somExplosao);
            TocarSom(somChiadoAgudo);

            collision.GetComponentInParent<Enemy>()?.TomarDano(dano);
            Destroy(gameObject);
            return;
        }

        // ===== BOSS =====
        if (collision.CompareTag("Boss"))
        {
            TocarSom(somExplosaoPequena);
            TocarSom(somChiadoGrave);

            collision.GetComponentInParent<Boss>()?.TomarDano(dano);
            Destroy(gameObject);
        }
    }

    void TocarSom(AudioClip clip)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}