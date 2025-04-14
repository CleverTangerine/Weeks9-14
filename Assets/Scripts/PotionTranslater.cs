using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class PotionTranslater : MonoBehaviour
{
    public UnityEvent CheckObjectPos;
    public int potionIndex = 0;
    public int objectIndex = 0;
    public MagicSpawner spawner;
    PotionScript potionScript;
    ObjectScript objectScript;

    public void IsMagicHappening()
    {
        CheckObjectPos.Invoke();
        if (objectIndex > 0 && objectIndex < 6)
        {
            objectScript = spawner.objectList[objectIndex].GetComponent<ObjectScript>();
            potionScript = spawner.potionList[potionIndex].GetComponent<PotionScript>();
            objectScript.StartListening(potionScript);
        }
        potionScript.BeingPutDown();
    }
}
