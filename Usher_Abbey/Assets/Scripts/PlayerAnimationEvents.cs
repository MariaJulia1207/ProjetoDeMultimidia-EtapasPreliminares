using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerController player;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public void TocarTiro()
    {
        if (player != null)
            player.TocarTiro();
    }

    public void FinalizarMorte()
    {
        if (player != null)
            player.FinalizarMorte();
    }
}
