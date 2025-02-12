using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : Animal
{
    void Start()
    {
        animalName = "Flamingo";
        age = 3;
    }

    public override void Eat()
    {
        Debug.Log("Flamingo is eating fish!");
    }
    public override void Interact()
    {
        UiManager.Instance.SetText($"Calling Interact Func. {animalName} is interacting with you");
    }
    public override void Play()
    {
        UiManager.Instance.SetText($"Calling Play Func. {animalName} is playing with you");
    }
    public override void MakeSound()
    {
        Debug.Log("Flamingo is making sound");
    }

}
