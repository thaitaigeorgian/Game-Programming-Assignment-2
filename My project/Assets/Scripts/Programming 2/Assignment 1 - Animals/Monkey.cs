using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Animal
{
    void Start()
    {
        animalName = "Monkey";
        age = 3;
    }

    public override void Eat()
    {
        Debug.Log("Monkey is eating banana!");
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
        Debug.Log("Monkey is making sound");
    }

}
