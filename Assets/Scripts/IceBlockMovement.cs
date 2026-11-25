using UnityEngine;
using UnityEngine.InputSystem;

public class IceBlockMovement : MonoBehaviour
{
    [SerializeField] private float moveDistance = 150f;
    [SerializeField] private float moveSpeed = 5f;    

    private RectTransform rectTransform;
    private Vector2 startPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        float xOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        
        rectTransform.anchoredPosition = new Vector2(startPos.x + xOffset, startPos.y);
    }
}