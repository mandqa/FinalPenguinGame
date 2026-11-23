using UnityEngine;
using TMPro;
using System.Collections;

public class MGTriggers : MonoBehaviour
{
    [SerializeField] GameObject EInteractPrompt;
    public TMP_Text EInteractText;
    [SerializeField] string thisMG;
    
    private bool canInteract;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EInteractPrompt.SetActive(canInteract);

        if (canInteract && Input.GetKeyDown(KeyCode.E)) StartMiniGame();
    }

    void StartMiniGame()
    {
        Debug.Log(thisMG); 
        
    }
    
    private void OnTriggerEnter(Collider collision)
    {
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
        }
    }
}
