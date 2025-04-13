using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpGrower : MonoBehaviour
{
    public AnimationCurve curve;
    public float minSize = 0;
    public float maxSize = 1.5f;
    public float timer = 0;
    public bool startGrowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (startGrowing)
        {
            Grow();
        }
    }

    public void GrowButton()
    {
        startGrowing = true;
        timer = 0;
    }
    private void Grow()
    {
        if (timer < 1) { 
            timer = (timer += Time.deltaTime);
        } else
        {
            startGrowing = false;
        }
        transform.localScale = Vector3.one * maxSize * curve.Evaluate(timer); // The reason this works better than lerping because lerping can only go between the min and max; there's no overshoot
        //transform.localScale = Vector3.Lerp(Vector3.one * minSize, Vector3.one * maxSize, curve.Evaluate(timer)); // You can think of evaluate as the timer for the x of a graph, and the curve for the y
    }
}
