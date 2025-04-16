using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ObjectScript : MonoBehaviour
{
    // References to other scripts
    // These need to be assigned by the Magic Spawner, since Uninstantiated prefabs can't reference other scripts
    public PotionTranslater translater;
    public MagicSpawner spawner;
    public PotionScript potionScript;

    // startPos is the position this object has when it's Instantiated
    public Vector3 startPos;

    // The index of this object in Magic Spawners objectArray
    public int objectType;

    // The index of a potion in Magic Spawners objectArray, to be referenced when listening to potionReturning
    public int potionID;

    // time is used to create timers
    // If I'm reusing it so much, might as well declare it publicly
    public float time;

    // Boolean to track if the mouse is over this object
    public bool mouseIsOver;

    // A curve to make animations look nicer
    public AnimationCurve curve;

    // A coroutine variable, used when the object Resets
    Coroutine transmogrify;

    // Start is called before the first frame update
    private void Start()
    {
        // Subscribes to different events at the start of the script
        translater.CheckObjectPos.AddListener(IsMouseHovering);
        translater.Resetting.AddListener(beginReset);

        // Tracks the position the object spawned at
        startPos = transform.position;
    }

    // These 2 events run when "PointerEnter" and "PointerExit," essentially tracking whether the mouse is over the object or not
    // The reason a boolean is used instead of just updating Potion Translater is because of overlap
    // If the mouse exits one object while its still in another, the value will be falsely set to 0
    public void MouseIsAbove()
    {
        mouseIsOver = true;
        //Debug.Log("mouse entered");
    }

    public void MouseIsntAbove()
    {
        mouseIsOver = false;
        //Debug.Log("mouse exited");
    }

    // This function tells the Potion Translater this object's index, but only if the mouse is over the object
    public void IsMouseHovering()
    {
        if (mouseIsOver)
        {
            translater.objectIndex = objectType;
            potionID = translater.potionIndex;
        }
    }

    // This function takes a PotionScript argument, and startsListening to the argument
    // The function is only called when the potion was placed on this object
    // The event it's listening for only runs when the potion is done pouring
    public void StartListening(PotionScript script)
    {
        potionScript = script;
        potionScript.potionReturning.AddListener(beginTransmogrify);
    }

    // The function is used to start a coroutine (since listeners can't start coroutines themselves)
    // It also tells this object to stop listening to the potion beingpoured on it,
    // so it doesn't accidentally run when the same potion is being poured onto a different object
    void beginTransmogrify()
    {
        potionScript.potionReturning.RemoveListener(beginTransmogrify);
        transmogrify = StartCoroutine(Transmogrify());
    }

    // Transmogrify is the function that makes the potion effect the appearene of the objects
    IEnumerator Transmogrify()
    {
        // Resets the time variable for the timer below
        time = 0;

        // scale and rot get the transform values before the animations
        Vector3 scale = transform.localScale;
        Vector3 rot = transform.eulerAngles;

        // scaleRNG and rotRNG are the random values transform will Lerp to
        Vector3 scaleRNG = Vector3.one * Random.Range(10, 130);
        Vector3 rotRNG = new Vector3(0, 0, Random.Range(0, 360));

        // imageCount is a counter that's based off of time
        // It will reset its value multiple times throughout this coroutine, so I made imageCount its own variable
        float imageCount = 0;

        // A timer for 2 seconds
        while (time < 2)
        {
            // time increases by deltaTime in order for the timer to actally finish in 1.5 seconds, instead of a random amount of frames
            time += Time.deltaTime;

            // This switch checks the potionID to see what kind of transformations are supposed to be applied
            switch (potionID)
            {
                // case 1 makes the object change size
                case 1:
                    transform.localScale = Vector3.Lerp(scale, scaleRNG, curve.Evaluate(time));
                    break;

                // case 2 makes the object randomly rotate
                case 2:
                    transform.eulerAngles = Vector3.Lerp(rot, rotRNG, curve.Evaluate(time));
                    break;

                // case 3 makes the object cycle through random sprites
                case 3:
                    // imageCount increases with the curve to make it more dynamic
                    imageCount += curve.Evaluate(time);
                    // Once imageCount has increased enough, it will pick a random sprite to display
                    if (imageCount > 50)
                    {
                        // The object references its own Image component, and picks a random sprite from the spawner's list of sprites
                        this.GetComponent<Image>().sprite = spawner.objectSprite[Random.Range(0, spawner.objectSprite.Length)];

                        // Then, imageCount is reset
                        imageCount -= 50;
                    }
                    break;
            }
            // This stops the script for one frame, and continues it on the next
            // It lets this script run for multiple frames, instead of the animation all happening at once
            yield return null;
        }
    }

    // The function is used to start a coroutine (since listeners can't start coroutines themselves)
    public void beginReset()
    {
        StopCoroutine(transmogrify);
        StartCoroutine(ResetObjects());
    }

    // This couroutine tells the object to reset itself to look like when it was instantiated
    IEnumerator ResetObjects()
    {
        // Resets the time variable for the timer below
        time = 0;

        // The object resets its image by referencing its index in the spawner
        this.GetComponent<Image>().sprite = spawner.objectSprite[objectType];

        // These rot and scale store the current transform values of the object
        Vector3 rot = transform.eulerAngles;
        Vector3 scale = transform.localScale;

        // Timer for 2 seconds
        while (time < 2)
        {
            // time increases by deltaTime in order for the timer to actally finish in 1.5 seconds, instead of a random amount of frames
            time += Time.deltaTime;


            // The transformations values are then translated back to normal
            transform.eulerAngles = Vector3.Lerp(rot, Vector3.zero, curve.Evaluate(time));
            transform.localScale = Vector3.Lerp(scale, Vector3.one * 80, curve.Evaluate(time));

            // This stops the script for one frame, and continues it on the next
            // It lets this script run for multiple frames, instead of the animation all happening at once
            yield return null;
        }
    }
}
