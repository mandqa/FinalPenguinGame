using UnityEngine;
using TMPro;

public class MGTriggers : MonoBehaviour
{
    [SerializeField] GameObject EInteractPrompt;
    public TMP_Text EInteractText;
    [SerializeField] public string thisMG;

    public bool canInteract;

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        EInteractPrompt.SetActive(false);
    }

    public void ShowPrompt()
    {
        if (canInteract)
        {
            EInteractPrompt.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            EInteractText.text = "Press [E] to " + thisMG;
            EInteractPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            EInteractPrompt.SetActive(false);
        }
    }
}