using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public Image[] coracoes;
    public Sprite coracaoCheio;
    public Sprite coracaoVazio;

    public void AtualizarHUD(int vidaAtual)
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            if (i < vidaAtual)
                coracoes[i].sprite = coracaoCheio;
            else
                coracoes[i].sprite = coracaoVazio;
        }
    }
}
