using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopShape : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    public void Grow()
    {
        StartCoroutine(GetBigger());
    }

    IEnumerator GetBigger() // IEnumerator runs once, on it's own. It can pause with "yield return null" and resume next frame
    {
        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.one * timer;
            yield return null;
        }

        
    }
}
