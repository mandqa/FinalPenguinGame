using UnityEngine;
using Unity.Cinemachine;

public class MGCameraViews : MonoBehaviour
{
    public CinemachineCamera playerCam;
    public CinemachineCamera[] minigameCams;

    public MGTriggers fishTrigger;
    public MGTriggers bushTrigger;
    public MGTriggers iceTrigger;

    [SerializeField] FishingMiniGame fmg;
    [SerializeField] IceCube iceGame;
    [SerializeField] PlayerMovement pm;

    private bool isInMinigameView = false;
    private string activeMG = "";

    void Start()
    {
        ResetToPlayerCamera();
    }

    void Update()
    {
        if (!isInMinigameView && Input.GetKeyDown(KeyCode.E))
        {
            if (fishTrigger.canInteract)
            {
                pm.FreezePlayer();
                fmg.StartFishing();
                activeMG = "Fish";
                SwitchToMinigameCamera(0);
            }
            else if (iceTrigger.canInteract)
            {
                pm.FreezePlayer();
                iceGame.StartIce();
                activeMG = "Ice";
                SwitchToMinigameCamera(2);
            }
        }

        if (isInMinigameView)
        {
            if (activeMG == "Fish" && fmg.endFishing)
            {
                EndMinigame(fishTrigger);
            }
            else if (activeMG == "Ice" && iceGame.endIce)
            {
                EndMinigame(iceTrigger);
            }
        }
    }

    void EndMinigame(MGTriggers trigger)
    {
        ResetToPlayerCamera();
        pm.UnfreezePlayer();
        trigger.ShowPrompt();
        activeMG = "";
    }

    void SwitchToMinigameCamera(int camIndex)
    {
        if (camIndex < minigameCams.Length)
        {
            minigameCams[camIndex].Priority = 20;
        }

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