using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBAnimation : MonoBehaviour
{
    public AnimationCurve curve1;
    public AnimationCurve curve2;

    [Range(0, 1)]
    public float animNum = 0;
    public float xOffset = -5;

    public Button button;
    public bool startAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.Is)
        {
            startAnim = false;
            StartCoroutine(Animation1());
        }
    }

    public IEnumerator Animation1()
    {
        while(animNum < 1)
        {
            animNum += Time.deltaTime / 1;
            Vector2 pos = transform.position;
            pos.x = xOffset + curve1.Evaluate(animNum);
            pos.y = curve2.Evaluate(animNum);
            transform.position = pos;
            yield return null;
        }
        button.enabled = true;
    }
}
