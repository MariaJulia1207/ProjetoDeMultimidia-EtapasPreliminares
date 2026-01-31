using UnityEngine;

public class Projetil : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade = 10f;
    public int dano = 1;
    public float tempoDeVida = 3f;

    [Header("Áudio")]
    public AudioClip somExplosaoPequena;
    public AudioClip somChiadoAgudo;
    public AudioClip somChiadoGrave;

    private float direcao = 1f;
    private bool jaColidiu;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
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
        if (jaColidiu) return;

        // ❌ ignora o próprio player
        if (collision.CompareTag("Player")) return;

        jaColidiu = true;

        // ===== INIMIGO COMUM =====
        if (collision.CompareTag("Enemy"))
        {
            TocarSom(somExplosaoPequena);
            TocarSom(somChiadoAgudo);

            collision.GetComponent<Enemy>()?.TomarDano(dano);
            Destroy(gameObject);
            return;
        }

        // ===== BOSS =====
        if (collision.CompareTag("Boss"))
        {
            TocarSom(somExplosaoPequena);
            TocarSom(somChiadoGrave);

            collision.GetComponent<Boss>()?.TomarDano(dano);
            Destroy(gameObject);
            return;
        }

        // ===== CHÃO / PAREDE =====
        if (collision.CompareTag("Ground"))
        {
            TocarSom(somExplosaoPequena);
            Destroy(gameObject);
        }
    }

    void TocarSom(AudioClip clip)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
