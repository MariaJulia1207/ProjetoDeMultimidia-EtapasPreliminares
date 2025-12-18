using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject quadradoPrefab;
    public float intervalo = 2f;
    private float tempoProximoSpawn;
    
    void Start()
    {

    }
    void Update()
    {
        if (Time.time >= tempoProximoSpawn)
        {
            SpawnarQuadrado();
            tempoProximoSpawn = Time.time + intervalo;
        }
    }

    void SpawnarQuadrado()
    {
        // Setando gravidade aleatória entre 0.5 e 10:
        quadradoPrefab.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5f,10f);
        
        // Define largura da tela
        float larguraTela = Camera.main.orthographicSize * Camera.main.aspect;
        float posX = Random.Range(-larguraTela, larguraTela);
        Vector2 posicaoSpawn = new Vector2(posX, Camera.main.orthographicSize + 1f);
        GameObject novoQuadrado = Instantiate(quadradoPrefab, posicaoSpawn, Quaternion.identity);
        
    }
}
