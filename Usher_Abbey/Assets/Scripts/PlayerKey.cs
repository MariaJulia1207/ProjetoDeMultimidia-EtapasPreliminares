using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public bool temChave { get; private set; }

    public void ColetarChave()
    {
        temChave = true;
    }

    public void UsarChave()
    {
        temChave = false;
    }
}
