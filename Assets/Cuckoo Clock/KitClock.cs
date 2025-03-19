using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;

    public UnityEvent<int> OnTheHour;

    Coroutine ClockIsRunning;
    Coroutine DoingOneHourOfMovement;

    void Start()
    {
        ClockIsRunning = StartCoroutine(MoveTheClock());
    }

    IEnumerator MoveTheClock()
    {
        while (true)
        {
            yield return DoingOneHourOfMovement = StartCoroutine(MoveTheClockHandOneHour());
        }
    }

    IEnumerator MoveTheClockHandOneHour()
    {
        t = 0;
        while(t < timeAnHourTakes)
        {
            t += Time.deltaTime;
            minuteHand.Rotate(0, 0, -(360/timeAnHourTakes) * Time.deltaTime);
            hourHand.Rotate(0, 0, -(30 / timeAnHourTakes) * Time.deltaTime);
            yield return null;
        }
        hour = hour % 12 + 1;
        OnTheHour.Invoke(hour);
    }

    public void StopTheClock()
    {
        StopCoroutine(ClockIsRunning);
        StopCoroutine(DoingOneHourOfMovement);
    }
}
