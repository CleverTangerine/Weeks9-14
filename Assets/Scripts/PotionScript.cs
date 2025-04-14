using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionScript : MonoBehaviour
{
    public PotionTranslater translater;
    public UnityEvent potionReturning;
    public int potionType = 0;
    public Vector3 originalPos;
    public Vector3 startPos;
    public AnimationCurve curveX;
    public AnimationCurve curveY;
    public AnimationCurve curveR;

    public float time = 0;
    
    public void BeingPickedUp()
    {
        Vector2 pos = transform.position;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    public void BeingPutDown()
    {
        startPos = transform.position;
        if (translater.objectIndex != 0)
        {
            //Start animation
        }
        else
        {
            StartCoroutine(SlidePotionBack());
        }
    }

    public IEnumerator SlidePotionBack()
    {
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, originalPos, time);
            yield return null;
        }
        time = 0;
    }

    IEnumerator PourPotion()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;
        while (time < 2)
        {
            time += Time.deltaTime;
            pos.x = startPos.x + curveX.Evaluate(time);
            pos.y = startPos.y + curveY.Evaluate(time);
            transform.position = pos;

            rot.x = curveR.Evaluate(time);
            transform.eulerAngles = rot;
            yield return null;
        }
        SlidePotionBack();
    }

}
