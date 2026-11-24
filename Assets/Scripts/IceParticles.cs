using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class IceParticle : MonoBehaviour
{
    private Rigidbody2D rb;
    private float killY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        RectTransform rt = (RectTransform)transform;
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.size = rt.rect.size;
        box.offset = rt.rect.center;
    }

    public void Launch(Vector2 worldVelocity, float angularVelocity, float killWorldY)
    {
        killY = killWorldY;
        rb.linearVelocity = worldVelocity; 
        rb.angularVelocity = angularVelocity;
    }

    private void Update()
    {
        if (transform.position.y < killY)
        {
            Destroy(gameObject);
        }
    }
}