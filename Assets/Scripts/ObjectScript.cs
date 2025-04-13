using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class Object : MonoBehaviour
{
    public PotionTranslater translater;
    public int potionType;
    public UnityEvent potionHasHitSomething;
    public SpriteRenderer sr;

    public void potionDrop()
    {
        if (sr.bounds.Contains(Input.mousePosition)){
            potionHasHitSomething.Invoke();
            translater.potionIndex = potionType;

            if (potionType != 0)
            {
                StartCoroutine(transmogrify());
            }
        }
    }

    IEnumerator transmogrify()
    {
        while (true)
        {

        }
        yield return null;
    }
}
