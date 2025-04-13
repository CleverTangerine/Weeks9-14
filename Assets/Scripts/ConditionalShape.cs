using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalShape : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    void Start()
    {
        //StartCoroutine(GetBigger());
    }

    public void GetBigger()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.one * timer;
        }

        
    }
}
