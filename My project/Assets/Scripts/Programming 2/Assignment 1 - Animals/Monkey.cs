using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Animal
{
    public override void _Start()
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
        // UiManager.Instance.SetText($"Calling Interact Func. {animalName} is interacting with you");
        transform.Rotate(0, Time.deltaTime * 50.0f < 10f ? 10f : Time.deltaTime * 50.0f, Time.deltaTime * 100.0f);
    }
    public override void Play()
    {
        UiManager.Instance.SetText($"Calling Play Func. {animalName} is playing with you");
    }
    public override void MakeSound()
    {
        UiManager.Instance.SetText($"{animalName} is making sound: Oo oo aa aa!");
    }

}
