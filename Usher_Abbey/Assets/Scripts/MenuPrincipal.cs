using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Configuração de Cena")]
    [SerializeField] private string nomeCenaJogo = "CenaJogo";

    [Header("Painéis")]
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelAjuda;

    private void Start()
    {
        // Garante que o menu abre corretamente
        painelMenu.SetActive(true);
        painelAjuda.SetActive(false);
    }

    public void BotaoIniciarJogo()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IniciarJogo();
        }

        SceneManager.LoadScene(nomeCenaJogo);
    }

    public void BotaoAjuda()
    {
        painelMenu.SetActive(false);
        painelAjuda.SetActive(true);
    }

    public void BotaoFecharAjuda()
    {
        painelAjuda.SetActive(false);
        painelMenu.SetActive(true);
    }

    public void BotaoSair()
    {
        Application.Quit();
    }
}
