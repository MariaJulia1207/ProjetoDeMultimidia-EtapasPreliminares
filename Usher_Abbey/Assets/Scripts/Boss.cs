using UnityEngine;

public class BossController : MonoBehaviour
{
    public float velocidade = 2f;
    public int vida = 10;

    public GameObject projetilPrefab;
    public Transform pontoDisparo;
    public float tempoEntreTiros = 2f;

    private Transform player;
    private float contadorTiro;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        SeguirPlayer();
        Atirar();
    }

    void SeguirPlayer()
    {
        Vector2 direcao = player.position - transform.position;
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            velocidade * Time.deltaTime
        );

        transform.localScale = new Vector3(
            direcao.x > 0 ? 1 : -1,
            1,
            1
        );
    }

    void Atirar()
    {
        contadorTiro -= Time.deltaTime;

        if (contadorTiro <= 0)
        {
            GameObject proj = Instantiate(
                projetilPrefab,
                pontoDisparo.position,
                Quaternion.identity
            );

            Vector2 dir = (player.position - pontoDisparo.position);
            proj.GetComponent<BossFireball>()
                .DefinirDirecao(dir);

            contadorTiro = tempoEntreTiros;
        }
    }

    public void TomarDano(int dano)
    {
        vida -= dano;

        if (vida <= 0)
            Destroy(gameObject);
    }
}
