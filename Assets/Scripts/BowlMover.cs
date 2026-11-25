using System.Collections;
using UnityEngine;

public class BowlMover : MonoBehaviour
{
    [SerializeField] private Transform gamePosition;
    [SerializeField] private Transform statusPosition;
    [SerializeField] private Transform maskContainer;
    [SerializeField] private float moveDuration = 0.6f;

    private Coroutine moveRoutine;

    private void Start()
    {
        transform.position = statusPosition.position;
        transform.localScale = statusPosition.localScale;
        SetPhysics(false);
    }

    public void MoveToGame()
    {
        StartMove(gamePosition, true);
    }

    public void MoveToStatus()
    {
        StartMove(statusPosition, false);
    }

    private void StartMove(Transform target, bool physicsOnAtEnd)
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }

        moveRoutine = StartCoroutine(MoveTo(target, physicsOnAtEnd));
    }

    private IEnumerator MoveTo(Transform target, bool physicsOnAtEnd)
    {
        SetPhysics(false);

        Vector3 fromPos = transform.position;
        Vector3 fromScale = transform.localScale;

        for (float t = 0f; t < moveDuration; t += Time.deltaTime)
        {
            float k = Mathf.SmoothStep(0f, 1f, t / moveDuration);
            transform.position = Vector3.Lerp(fromPos, target.position, k);
            transform.localScale = Vector3.Lerp(fromScale, target.localScale, k);
            yield return null;
        }

        transform.position = target.position;
        transform.localScale = target.localScale;
        SetPhysics(physicsOnAtEnd);
        moveRoutine = null;
    }

    private void SetPhysics(bool on)
    {
        foreach (Rigidbody2D r in maskContainer.GetComponentsInChildren<Rigidbody2D>())
        {
            r.simulated = on;
        }
    }
}