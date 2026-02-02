using UnityEngine;

public class BossAttackArea : MonoBehaviour
{
    private Boss boss;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            boss.JogadorNaArea(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            boss.JogadorNaArea(false);
    }
}
