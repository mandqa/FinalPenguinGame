using UnityEngine;

public class FishingRodManager : MonoBehaviour
{
    [SerializeField] FishingMiniGame fmg;

    public float multiplierVariable = 100f;


    public float snapSpeed = 12f;
    public float jerkIntensity = 2.5f;

    float previousHookProgress;
    float targetAngle;
    float currentAngle;

    [SerializeField] float minTargetAngle = 0f;
    [SerializeField] float maxTargetAngle = -40f;

    void Start()

    {
        if (fmg != null)
        {
            previousHookProgress = fmg.hookProgress;
        }
        
        currentAngle = transform.localEulerAngles.z;
        targetAngle = currentAngle;
    }

    void Update()
    {
        if (fmg.paused) return;

        fightFish();
    }

    void fightFish()
    {
        if (fmg == null) return;
        
        float progress = fmg.hookProgress - previousHookProgress;

        if (Mathf.Abs(progress) > 0.0001f)
        {
            targetAngle -= progress * multiplierVariable;
            float jerk = Random.Range(-jerkIntensity, jerkIntensity) * Mathf.Abs(progress) * 10f;
            targetAngle += jerk;
        }
        
        targetAngle = Mathf.Clamp(targetAngle, minTargetAngle, maxTargetAngle);
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * snapSpeed);
        
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, currentAngle);

        previousHookProgress = fmg.hookProgress;
    }

}
