using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movimento")]
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 2f;

    [Header("Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDisparo;
    public float tempoEntreTiros = 2f;

    [Header("Vida")]
    public int vida = 10;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip somPasso;
    public AudioClip somDano;
    public AudioClip somMorte;

    private Transform alvoAtual;
    private Animator anim;
    private bool morto;

    private bool jogadorNaArea;
private float tempoTiro;


    void Start()
    {
        anim = GetComponent<Animator>();
        alvoAtual = pontoB;
    }

    void Update()
    {
        if (morto) return;

    Andar();

    if (jogadorNaArea)
        Atacar();
    }

    void Andar()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            alvoAtual.position,
            velocidade * Time.deltaTime
        );

        anim.SetBool("walking", true);

        if (Vector2.Distance(transform.position, alvoAtual.position) < 0.1f)
{
    if (alvoAtual == pontoA)
        alvoAtual = pontoB;
    else
        alvoAtual = pontoA;

    float direcao = Mathf.Sign(alvoAtual.position.x - transform.position.x);
    transform.localScale = new Vector3(direcao, 1, 1);
}

    }

    void Atacar()
    {
        tempoTiro += Time.deltaTime;

    if (tempoTiro >= 2f)
    {
        anim.SetTrigger("attack");

        GameObject proj = Instantiate(
            projetilPrefab,
            pontoDisparo.position,
            Quaternion.identity
        );

        proj.GetComponent<BossProjectile>()
            .DefinirDirecao(transform.localScale.x);

        tempoTiro = 0f;
    }
    }

    public void TomarDano(int dano)
    {
        if (morto) return;

        vida -= dano;
        anim.SetTrigger("hurt");
        audioSource.PlayOneShot(somDano);
        GetComponent<DamageFlash>()?.Flash();


        if (vida <= 0)
            Morrer();
    }

    void Morrer()
    {
        morto = true;
        anim.SetTrigger("death");
        audioSource.PlayOneShot(somMorte);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    // Animation Event
    public void TocarPasso()
    {
        audioSource.PlayOneShot(somPasso);
    }

    public void JogadorNaArea(bool estado)
{
    jogadorNaArea = estado;
}


}

