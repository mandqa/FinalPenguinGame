using UnityEngine;
using UnityEngine.InputSystem;

public class BowlController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    
    [SerializeField] private float maxSpeed = 900f;    
    [SerializeField] private float smoothTime = 0.12f;  

    private Rigidbody2D rb;
    private RectTransform rt;
    private RectTransform parentRect;

    private float targetLocalX;
    private float currentLocalX;
    private float velocityX; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        rt = (RectTransform)transform;
        parentRect = (RectTransform)rt.parent;

        currentLocalX = targetLocalX = rt.localPosition.x;
    }

    private void Update()
    {
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Mouse.current.position.ReadValue(), cam, out Vector2 local);

        float half = rt.rect.width * 0.5f;
        targetLocalX = Mathf.Clamp(local.x, parentRect.rect.xMin + half, parentRect.rect.xMax - half);
    }

    private void FixedUpdate()
    {
        currentLocalX = Mathf.SmoothDamp(currentLocalX, targetLocalX, ref velocityX, smoothTime, maxSpeed, Time.fixedDeltaTime);

        Vector3 target = parentRect.TransformPoint(new Vector3(currentLocalX, rt.localPosition.y, 0f));
        rb.MovePosition(target);
    }
}