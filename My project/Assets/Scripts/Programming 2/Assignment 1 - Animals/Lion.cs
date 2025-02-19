using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Animal
{
    public override void _Start()
    {
        animalName = "Lion";
        age = 3;
    }

    public override void Eat()
    {
        Debug.Log("Lion is eating meat!");
    }
    public override void Interact()
    {
        // UiManager.Instance.SetText($"Calling Interact Func. {animalName} is interacting with you");
        transform.Rotate(0, 0, Time.deltaTime * 100.0f);
    }
    public override void Play()
    {
        UiManager.Instance.SetText($"Calling Play Func. {animalName} is playing with you");
    }
    public override void MakeSound()
    {
        UiManager.Instance.SetText($"{animalName} is making sound: Roarrrr!");
    }

}
