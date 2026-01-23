using UnityEngine;

public class RotatingPlatformController : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public bool clockwise = true;

    void FixedUpdate()
    {
        float dir = clockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, dir * rotationSpeed * Time.fixedDeltaTime);
    }

    // ================= PLAYER EM CIMA =================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                collision.transform.SetParent(transform);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.transform.parent == transform)
        {
            collision.transform.SetParent(null);
        }
    }
}
