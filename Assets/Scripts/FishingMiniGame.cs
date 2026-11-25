using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;   

public class FishingMiniGame : MonoBehaviour
{
    [SerializeField] GameObject FishingUI;
    
    [SerializeField] FishingRodManager rod;
    
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [SerializeField] float timeMultiplicator = 3f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;
    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 5f;
    public float hookProgress;
    float hookPullVelocity;
    
    [SerializeField] Image hookUIImage; 
    
    [SerializeField] float hookPullPower;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDegradationPower = 0.1f;

    [SerializeField] float maxVelocity = 0.02f;

    [SerializeField] Transform progressBarContainer;

    [SerializeField] Image fishImage;
    
    public bool endFishing = false;
    
    [SerializeField] StatusBarManager  statusBarManager;

    /*private void Start()
    {
        Resize();
        
        StartFishing();
    }*/

    public void StartFishing()
    {
        endFishing = false;
        fishImage.enabled = false;

        hookProgress = 0.2f;
        hookPosition = 0.5f;
        hookPullVelocity = 0f;

        fishPosition = Random.Range(0.2f, 0.8f);
        fishDestination = Random.Range(0f, 1f);
        fishTimer = 0f;
        fishSpeed = 0f;

        if (FishingUI != null)
        {
            FishingUI.SetActive(true);
        }

        Resize();
        
    }

    public void FinishFishing(bool caughtFish)
    {
        endFishing = true;
        if (caughtFish)
        {
            fishImage.enabled = true;
        }
        
        Invoke(nameof(CloseMiniGame), 1.5f);
    }

    private void CloseMiniGame()
    {
        FishingUI.SetActive(false);
    }

    private void Update()
    {
        if (endFishing) return;
        
        Fish();
        Hook();
        ProgressCheck();
    }

    private void Resize()
    {
        RectTransform hookRect = hookUIImage.rectTransform;

        float ySize = hookRect.rect.height; 

        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize) * hookSize;
        hook.localScale = ls;
    }

    void Hook()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            hookPullVelocity += hookPullPower *Time.deltaTime;
        }
        else
        {
            hookPullVelocity -= hookGravityPower * Time.deltaTime;
        }
        
        hookPullVelocity = Mathf.Clamp(hookPullVelocity, -maxVelocity, maxVelocity);
        
        float minPosition = hookSize / 2f - 0.04f;
        float maxPosition = 1f - hookSize / 2f + 0.04f;
        
        hookPosition += hookPullVelocity;
        
        if (hookPosition >= maxPosition)
        {
            hookPosition = maxPosition;
            hookPullVelocity = 0f; 
        }
        else if (hookPosition <= minPosition)
        {
            hookPosition = minPosition;
            hookPullVelocity = 0f; 
        }

        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
        
    }

    void Fish()
    {
        fishTimer -= Time.deltaTime;
        
                if (fishTimer < 0f)
                {
                    fishTimer = UnityEngine.Random.value * timeMultiplicator;
        
                    fishDestination = UnityEngine.Random.value;
                }
        
                fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
                
                fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }

    private void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;
        }
        
        if (hookProgress >= 1f)
        {
            Win();
        }
        
        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);

        
    }

    private void Win()
    {
        //Debug.Log("you caught it");
        endFishing = true;
        statusBarManager.HealFishy(50);
        
        FishingUI.SetActive(false);
        
    }
}
