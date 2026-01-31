/*using UnityEngine;

public class MusicAreaBoss : MonoBehaviour
{
    public AudioSource musicaBoss;
    public int prioridade = 10;
    public Boss boss;

    private bool playerNaArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (boss == null || boss.EstaMorto) return;

        playerNaArea = true;
        MusicManager.Instance.TocarMusica(musicaBoss, prioridade);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerNaArea = false;
        MusicManager.Instance.PararMusica(musicaBoss, prioridade);
    }

    public void BossMorreu()
    {
        if (playerNaArea)
            MusicManager.Instance.PararMusica(musicaBoss, prioridade);
    }
}
*/