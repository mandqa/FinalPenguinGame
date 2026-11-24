using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class IceCube : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private IceParticle particlePrefab;
    [SerializeField] private RectTransform particleParent;
    
    [SerializeField] private int minParticles = 3;
    [SerializeField] private int maxParticles = 5;

    [SerializeField] private Vector2 xSpeedRange = new Vector2(-150f, 150f);
    [SerializeField] private Vector2 ySpeedRange = new Vector2(-150f, -30f);
    
    [SerializeField] private float maxSpin = 360f;
    [SerializeField] private float spawnJitter = 20f;
    
    [SerializeField] private int clicksToBreak = 10;
    [SerializeField] private float shrinkDuration = 0.15f;  
    [SerializeField] private float regrowDuration = 3f;    
    
    [SerializeField, Range(0f, 0.5f)] private float regrowStartScale = 0.1f; 

    private Vector3 baseScale;
    private int clicks;
    private bool canClick = true;
    private Coroutine scaleRoutine;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canClick) return;

        SpawnParticles(eventData);

        clicks++;

        if (clicks >= clicksToBreak)
        {
            canClick = false;
            StartScale(BreakAndRegrow());
        }
        else
        {
            float targetFactor = 1f - (float)clicks / clicksToBreak;
            StartScale(ScaleTo(targetFactor, shrinkDuration));
        }
    }

    private void SpawnParticles(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(particleParent, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        float scale = particleParent.lossyScale.x;

        Vector3[] corners = new Vector3[4];
        particleParent.GetWorldCorners(corners);
        float killY = corners[0].y - 100f * scale;

        int count = Random.Range(minParticles, maxParticles + 1);
        for (int i = 0; i < count; i++)
        {
            IceParticle p = Instantiate(particlePrefab, particleParent);
            
            Vector2 jitter = Random.insideUnitCircle * spawnJitter;
            p.GetComponent<RectTransform>().anchoredPosition = localPoint + jitter;

            Vector2 vel = new Vector2(Random.Range(xSpeedRange.x, xSpeedRange.y), Random.Range(ySpeedRange.x, ySpeedRange.y)) * scale;

            p.Launch(vel, Random.Range(-maxSpin, maxSpin), killY);
        }
    }

    private void StartScale(IEnumerator routine)
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(routine);
    }

    private IEnumerator ScaleTo(float factor, float duration)
    {
        Vector3 from = transform.localScale;
        Vector3 to = baseScale * factor;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(from, to, t / duration);
            yield return null;
        }
        transform.localScale = to;
    }

    private IEnumerator BreakAndRegrow()
    {
        yield return ScaleTo(0f, shrinkDuration);
        
        Vector3 from = baseScale * regrowStartScale;
        Vector3 to = baseScale;

        for (float t = 0f; t < regrowDuration; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(from, to, t / regrowDuration);
            yield return null;
        }

        transform.localScale = to;
        clicks = 0;
        canClick = true;
        scaleRoutine = null;
    }
}