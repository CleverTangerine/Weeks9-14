using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineGrower : MonoBehaviour
{
    public AnimationCurve curve;
    public float minSize = 0;
    public float maxSize = 1.5f;
    public float timer = 0;

    public Transform apple;

    public void GrowButton() // Coroutine's cannot be called from a button, it needs a function to start it
    {
        StartCoroutine(Grow()); // Without StartCoroutine, the IEnumerator won't run; it won't CRASH, but it won't RUN
    }
    private IEnumerator Grow()
    {
        apple.localScale = Vector3.zero;
        timer = 0;
        while (timer < 1)
        {
            timer = (timer += Time.deltaTime);
            transform.localScale = Vector3.one * maxSize * curve.Evaluate(timer); // The reason this works better than lerping because lerping can only go between the min and max; there's no overshoot
            yield return null;
        }

        timer = 0;
        while (timer < 1)
        {
            timer = (timer += Time.deltaTime);
            apple.localScale = Vector3.one * maxSize * curve.Evaluate(timer); // The reason this works better than lerping because lerping can only go between the min and max; there's no overshoot
            yield return null;
        }

        //transform.localScale = Vector3.Lerp(Vector3.one * minSize, Vector3.one * maxSize, curve.Evaluate(timer)); // You can think of evaluate as the timer for the x of a graph, and the curve for the y
    }
}
