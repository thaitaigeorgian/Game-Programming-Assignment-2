using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : Animal
{
    public override void _Start()
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
        // UiManager.Instance.SetText($"Calling Interact Func. {animalName} is interacting with you");
        transform.position = originalPosition + new Vector3(0, Mathf.Sin(Time.time) * 1.0f, 0);
    }
    public override void Play()
    {
        UiManager.Instance.SetText($"Calling Play Func. {animalName} is playing with you");
    }
    public override void MakeSound()
    {
        UiManager.Instance.SetText($"{animalName} is making sound: Honk! Honk!");
    }

}
