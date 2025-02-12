using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IPlayable
{
    void Play();
}

public abstract class Animal : MonoBehaviour, IInteractable, IPlayable
{
    public string animalName;
    public int age;
    bool isPlayerInside = false;

    void Update()
    {
        if (isPlayerInside && Input.GetKey(KeyCode.E))
        {
            Interact();
            transform.Rotate(0, 0, Time.deltaTime * 100.0f);
        }
        else if (isPlayerInside && Input.GetKey(KeyCode.P))
        {
            Play();
            transform.Rotate(0, 0, Time.deltaTime * 100.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.Instance.SetText($"Press E to interact with {animalName}\nPress P to play with {animalName}");
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UiManager.Instance.ClearText();
            isPlayerInside = false;
            transform.Rotate(0, 0, 0);
        }
    }

    public abstract void Eat();

    public abstract void Interact();
    public abstract void Play();

    public abstract void MakeSound();
}


