using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ObjectScript : MonoBehaviour
{
    public PotionTranslater translater;
    public PotionScript potionScript;
    public UnityEvent potionHasHitSomething;
    public SpriteRenderer sr;
    public int objectType;
    public int potionID;
    public float time;
    Vector3 startPos;
    public AnimationCurve[] curveX;
    public AnimationCurve[] curveY;
    public AnimationCurve[] curveR;

    private void Start()
    {
        translater.CheckObjectPos.AddListener(IsMouseHovering);
        startPos = transform.position;
    }

    public void IsMouseHovering()
    {
        if (sr.bounds.Contains(Input.mousePosition))
        {
            translater.objectIndex = objectType;
            potionID = translater.potionIndex;
        }
    }

    public void StartListening(PotionScript script)
    {
        potionScript = script;
        potionScript.potionReturning.AddListener(beginTransmogrify);
    }

    void beginTransmogrify()
    {
        potionScript.potionReturning.RemoveListener(beginTransmogrify);
        StartCoroutine(Transmogrify());
    }
    IEnumerator Transmogrify()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;
        while (time < 2)
        {
            time += Time.deltaTime;
            pos.x = startPos.x + curveX[potionID].Evaluate(time);
            pos.y = startPos.y + curveY[potionID].Evaluate(time);
            transform.position = pos;

            rot.x = curveR[potionID].Evaluate(time);
            transform.eulerAngles = rot;
            yield return null;
        }
    }

    
}
