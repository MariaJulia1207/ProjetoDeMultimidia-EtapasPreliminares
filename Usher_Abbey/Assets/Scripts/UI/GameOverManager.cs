using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    [SerializeField] private GameObject painelGameOver;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        painelGameOver.SetActive(false);
    }

    public void MostrarGameOver()
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}

