using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hippo : Animal
{
    void Start()
    {
        animalName = "Hippo";
        age = 3;
    }

    public override void Eat()
    {
        Debug.Log("Hippo is eating meat!");
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
        Debug.Log("Hippo is making sound");
    }

}
