using UnityEngine;
using TMPro;
using System.Collections;

public class MGTriggers : MonoBehaviour
{
    [SerializeField] GameObject EInteractPrompt;
    public TMP_Text EInteractText;
    [SerializeField] public string thisMG;
    [SerializeField] GameObject fishingMiniGame;
    [SerializeField] PlayerMovement playerMovement;
    
    private bool canInteract;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.FreezePlayer();
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        if (thisMG == "Fish")
        {
            fishingMiniGame.SetActive(true);
        }
        else if (thisMG == "Shave Ice")
        {
            EInteractText.text = "Press [E] to "+thisMG;
        }
        else if (thisMG == "Shake Bush")
        {
            EInteractText.text = "Press [E] to "+thisMG;
        }
        
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        EInteractPrompt.SetActive(canInteract);
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            if (thisMG == "Fish")
            {
                EInteractText.text = "Press [E] to "+thisMG;
            }
            if (thisMG == "Shave Ice")
            {
                EInteractText.text = "Press [E] to "+thisMG;
            }
            if (thisMG == "Shake Bush")
            {
                EInteractText.text = "Press [E] to "+thisMG;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            canInteract = false;
            playerMovement.UnfreezePlayer();
        }
        EInteractPrompt.SetActive(canInteract);
    }
}
