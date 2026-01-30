using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 3;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TomarDano(int dano)
    {
        vida -= dano;

        if (anim != null)
            anim.SetTrigger("hurt");

        if (vida <= 0)
            Morrer();
    }

    void Morrer()
    {
        if (anim != null)
            anim.SetTrigger("death");

        Destroy(gameObject, 0.2f);
    }
}
