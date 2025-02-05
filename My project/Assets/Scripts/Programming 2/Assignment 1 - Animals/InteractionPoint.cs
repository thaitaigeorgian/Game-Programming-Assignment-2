using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPoint : MonoBehaviour
{
    public GameObject interactionPrompt;
    public Text infoText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
        }
    }

    public void OnInteract()
    {
        infoText.text = "You interacted with the habitat.";
    }
}
