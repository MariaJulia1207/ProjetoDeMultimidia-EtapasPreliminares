using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    public float tempoFlash = 0.1f;

    private SpriteRenderer sr;
    private Color corOriginal;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        corOriginal = sr.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(tempoFlash);
        sr.color = corOriginal;
    }
}
