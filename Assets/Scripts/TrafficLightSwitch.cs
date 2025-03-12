using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrafficLightSwitch : MonoBehaviour
{
    public UnityEvent OnLight;
    public bool switchState = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switchState = !switchState;
            OnLight.Invoke();
        }
    }

}
