using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionScript : MonoBehaviour
{
    public UnityEvent<int> grabbed;
    public int potionType = 1;
    
    public void beingPickedUp()
    {
        grabbed.Invoke(potionType);
        Vector2 pos = transform.position;
        pos = Input.mousePosition;
        transform.position = pos;
    }

    public void beingPutDown()
    {
        
    }

}
