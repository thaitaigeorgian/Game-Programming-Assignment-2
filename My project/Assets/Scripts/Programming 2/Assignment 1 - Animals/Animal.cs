using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IMakeSoundable
{
    void MakeSound();
}

public abstract class Animal : MonoBehaviour, IInteractable, IMakeSoundable
{
    public string animalName;
    public int age;
    bool isPlayerInside = false;

    public Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        _Start();
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKey(KeyCode.E))
        {
            Interact();

        }
        else if (isPlayerInside && Input.GetKey(KeyCode.S))
        {
            // Play();
            MakeSound();
            // transform.Rotate(0, 0, Time.deltaTime * 100.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.Instance.SetText($"Hold E to interact with {animalName}\nPress S - animal make sound {animalName}");
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.Instance.ClearText();
            isPlayerInside = false;
            transform.Rotate(0, 0, 0);
        }
    }

    public abstract void Eat();
    public abstract void _Start();

    public abstract void Interact();
    public abstract void Play();

    public abstract void MakeSound();
}


