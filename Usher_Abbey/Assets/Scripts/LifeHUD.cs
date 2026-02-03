using UnityEngine;
using TMPro;

public class LifeHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoVidas;

    public void AtualizarVidas(int vidas)
    {
        if (textoVidas == null)
        {
            Debug.LogError("LifeHUD: textoVidas NÃO está atribuído!");
            return;
        }

        textoVidas.text = "Vidas = " + vidas;
    }
}
