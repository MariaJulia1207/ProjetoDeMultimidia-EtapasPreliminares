using UnityEngine;

public class HealthHUD : MonoBehaviour
{
    public HeartUI[] coracoes;
    public GameObject gameOverPanel;

    public void AtualizarHUD(int vidaAtual)
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            if (i < vidaAtual)
                coracoes[i].AtualizarVida(4); // cheio
            else
                coracoes[i].AtualizarVida(0); // vazio
        }

        if (vidaAtual <= 0)
            GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}
