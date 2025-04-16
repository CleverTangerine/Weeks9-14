using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PotionTranslater : MonoBehaviour
{
    // UnityEvent that checks if a potion is being placed over an object
    public UnityEvent CheckObjectPos;

    // UnityEvent that tells objects to revert their change
    public UnityEvent Resetting;

    // These Index ints store the index of the potion being grabbed, and the object about to be Transmogrified
    public int potionIndex = 0;
    public int objectIndex = 0;

    // Reference to the spawner script in order to access the Array of potion.object prefabs
    public MagicSpawner spawner;

    // References to the scripts for potions and objects
    // These will be used to grab and set certain values in these prefabs
    PotionScript potionScript;
    ObjectScript objectScript;

    // This function is used to check if a potion is over an object
    public void IsMagicHappening()
    {
        // Objects will listen to this event, and return whether or not the potion is over them
        CheckObjectPos.Invoke();

        // The index of the potion is used to reference the exact prefab that is being checked
        potionScript = spawner.potionArray[potionIndex].GetComponent<PotionScript>();

        // If an object confirms the potion is over it, it will update the objectIndex value (default is 0 value)
        // Then, the index will be used to tell the potion to startListening to when the potion finishes their pouring animations
        if (objectIndex > 0 && objectIndex <= 4)
        {
            objectScript = spawner.objectArray[objectIndex].GetComponent<ObjectScript>();
            objectScript.StartListening(potionScript);
        }
        // No matter what, the potion will be told to check what value this translater found
        potionScript.BeingPutDown();

        // The potion and object indices are then reset, so they're not reused the next time this is checked
        potionIndex = 0;
        objectIndex = 0;
    }

    // A function controlled by the "Reset Button"
    // The objects listen to the Resetting event, and proceed to reset their appearence
    public void ResetAllObjects()
    {
        Resetting.Invoke();
    }
}
