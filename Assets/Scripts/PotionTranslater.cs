using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PotionTranslater : MonoBehaviour
{
    public EventListener potionListener;
    public int potionIndex = 0;
    public PotionScript potionScript;

    private void Start()
    {
        potionScript.grabbed.AddListener(StorePotionValue);
        
    }

    public void StorePotionValue(int value)
    {
        potionIndex = value;
    }
}
