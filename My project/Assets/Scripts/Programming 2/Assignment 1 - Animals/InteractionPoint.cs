using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPoint : MonoBehaviour
{
    bool isPlayerInside = false;
    public string areaName;
    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UiManager.Instance.SetText($"You are in {areaName}");
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UiManager.Instance.ClearText();
            isPlayerInside = false;
        }
    }

    public void OnInteract()
    {
        Debug.Log("OnInteract");
    }
}
