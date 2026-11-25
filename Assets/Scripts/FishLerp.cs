using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishLerp : MonoBehaviour
{
    [SerializeField] private Image fishTemplate;
    [SerializeField] private Transform popupPosition;
    [SerializeField] private float popupScaleMultiplier = 2.5f;
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private float lerpDuration = 0.6f;
    [SerializeField] private float spacing = 10f;

    private Vector3 baseRestPosition;
    private Vector3 restScale;
    private int fishCount;

    private void Awake()
    {
        baseRestPosition = fishTemplate.transform.localPosition;
        restScale = fishTemplate.transform.localScale;
        fishTemplate.enabled = false;
    }

    public void Show()
    {
        fishCount++;

        Image fish = Instantiate(fishTemplate, fishTemplate.transform.parent);
        fish.transform.SetSiblingIndex(fishTemplate.transform.GetSiblingIndex() + fishCount);

        Vector3 restPosition = baseRestPosition + new Vector3(spacing * (fishCount - 1), 0f, 0f);
        StartCoroutine(PopAndSettle(fish, restPosition));
    }

    private IEnumerator PopAndSettle(Image fish, Vector3 restPosition)
    {
        Transform t = fish.transform;
        Vector3 startPos = t.parent.InverseTransformPoint(popupPosition.position);
        Vector3 startScale = restScale * popupScaleMultiplier;

        t.localPosition = startPos;
        t.localScale = startScale;
        fish.enabled = true;

        yield return new WaitForSeconds(holdDuration);

        for (float time = 0f; time < lerpDuration; time += Time.deltaTime)
        {
            float k = Mathf.SmoothStep(0f, 1f, time / lerpDuration);
            t.localPosition = Vector3.Lerp(startPos, restPosition, k);
            t.localScale = Vector3.Lerp(startScale, restScale, k);
            yield return null;
        }

        t.localPosition = restPosition;
        t.localScale = restScale;
    }
}