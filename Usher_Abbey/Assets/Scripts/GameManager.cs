using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool jogoIniciado;

    private void Awake()
    {
        // Garante que só exista um GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // não destrói ao trocar de cena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IniciarJogo()
    {
        jogoIniciado = true;
        Debug.Log("Jogo iniciado! GameManager ativo.");
    }
}
