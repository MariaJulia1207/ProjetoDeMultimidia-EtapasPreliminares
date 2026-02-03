using UnityEngine;

public class DeathCutsceneController : MonoBehaviour
{
    public static DeathCutsceneController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }

    public void IniciarCutscene()
    {
        gameObject.SetActive(true);
        Time.timeScale = 1f;
        FindObjectOfType<PlayerController>().enabled = false;

    }

    public void FinalizarCutscene()
    {
        Debug.Log("Cutscene finalizada");
        GameOverManager.Instance.MostrarGameOver();
    }
}
