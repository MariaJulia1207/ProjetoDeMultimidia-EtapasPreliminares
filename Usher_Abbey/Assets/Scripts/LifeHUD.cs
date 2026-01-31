using UnityEngine;
using TMPro;

public class LifeHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoVidas;

    public void AtualizarVidas(int vidas)
    {
        textoVidas.text = "Vidas = " + vidas;

        if (vidas <= 0)
        {
            GameOverManager.Instance.MostrarGameOver();
        }
    }
}
