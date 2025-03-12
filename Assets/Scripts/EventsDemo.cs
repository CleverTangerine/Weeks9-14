using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsDemo : MonoBehaviour
{
    public RectTransform banana;

    public UnityEvent OnTimerFinished;
    public float timerLength = 2;
    public float t;

    private void Update()
    {
        t += Time.deltaTime;
        if (t > timerLength)
        {
            OnTimerFinished.Invoke();
            t -= 2;
        }
        //if(t < timerLength)
        //{
        //    t += Time.deltaTime;
        //}
        //else
        //{
        //    OnTimerFinished.Invoke();
        //}
    }

    public void MouseJustEntered()
    {
        Debug.Log("Mouse just ENTERED me! <3");

        banana.localScale = Vector3.one * 1.2f;
    }
    public void MouseJustExited()
    {
        Debug.Log("Mouse just exited :0");

        banana.localScale = Vector3.one;
    }
}
