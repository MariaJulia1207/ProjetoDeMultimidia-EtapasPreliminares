using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public Animator anim;
    private int vidaAtual = 4;

    public void AtualizarVida(int vida)
    {
        vidaAtual = Mathf.Clamp(vida, 0, 4);
        anim.SetInteger("vida", vidaAtual);
    }
}
