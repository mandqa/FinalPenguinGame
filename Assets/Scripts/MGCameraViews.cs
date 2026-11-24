using UnityEngine;
using Unity.Cinemachine;

public class MGCameraViews : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera[] minigameCams; // 0: Fish, 1: Bush, 2: Ice

    public MGTriggers fishTrigger;
    public MGTriggers bushTrigger;
    public MGTriggers iceTrigger;

    private bool isInMinigameView = false;

    void Start()
    {
        ResetToPlayerCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInMinigameView)
            {
                // Return to normal player view if pressed again
                ResetToPlayerCamera();
            }
            else if (fishTrigger.thisMG == "Fish")
            {
                SwitchToMinigameCamera(0);
            }
            // Add your other triggers here using the same pattern:
            // else if (bushTrigger.thisMG == "Bush") SwitchToMinigameCamera(1);
        }
    }

    void SwitchToMinigameCamera(int camIndex)
    {
        minigameCams[camIndex].Priority = 20;
        isInMinigameView = true;
    }

    void ResetToPlayerCamera()
    {
        for (int i = 0; i < minigameCams.Length; i++)
        {
            minigameCams[i].Priority = 5;
        }
        playerCam.Priority = 10;
        isInMinigameView = false;
    }
}