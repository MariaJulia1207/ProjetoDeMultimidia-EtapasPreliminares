using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 2f;

    private Vector3 destinoAtual;

    void Start()
    {
        destinoAtual = pontoB.position;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * velocidade, 1);
transform.position = Vector3.Lerp(pontoA.position, pontoB.position, t);


        if (Vector3.Distance(transform.position, destinoAtual) < 0.1f)
        {
            destinoAtual = destinoAtual == pontoA.position
                ? pontoB.position
                : pontoA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
