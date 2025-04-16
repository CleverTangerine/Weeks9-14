using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionScript : MonoBehaviour
{
    // A UnityEvent that's Invoked when the potion is sliding back into its starting position
    // Objects which are set to be Transmogrified will listen to this event to determine when to start their own animations
    public UnityEvent potionReturning;

    // A reference to the Potion Translater
    public PotionTranslater translater;

    // startPos is the position this potion has when it's Instantiated
    public Vector3 startPos;

    // placedPos is the position the potion will start at when it starts its animations
    // It get locked in place once mouse is released
    public Vector3 placedPos;

    // The index and effect of this potion in Magic Spawners objectArray
    public int potionType = 0;

    // time is used to create timers
    // If I'm reusing it so much, might as well declare it publicly
    public float time = 0;

    // curves X (x position), Y (y position), and R (rotation) are for the potion pouring animation
    // The x position, y position, and rotation all move by different amounts in the animation
    public AnimationCurve curveX;
    public AnimationCurve curveY;
    public AnimationCurve curveR;

    // curveSlide is used when the potion slides back to its startPos
    public AnimationCurve curveSlide;

    // Start is called before the first frame update
    private void Start()
    {
        // Tracks the position the potion spawned at
        startPos = transform.position;
    }

    // This script is triggerd by the UnityEvent "PointerDown," which triggers when Left Click is held over the sprite
    // It's used to let the player drag the potion around
    public void BeingPickedUp()
    {
        // transform.position follows the mouse in worldspace
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    // This script is triggerd by the UnityEvent "PointerUp," which triggers when Left Click is released over the sprite
    // While I'd want to directly reference Potion Translater, uninstantiated prefabs cam't reference other scripts
    // Thus, we get the reference after it's Instantiated, and then call it
    public void CheckWithTranslater()
    {
        // This tells the Potion Translater which potion was being held
        translater.potionIndex = potionType;
        translater.IsMagicHappening();
    }

    // BeingPutDown runs after the Potion Translater can tell the potion that was just held whether or not it landed on an object
    // Potion Translater can't confirm with the potion the moment its placed because:
    // The translater needs a frame to tell the objects to check their position, and the potion needs the frame to get a response
    // It can't happen on the same frame
    public void BeingPutDown()
    {
        // Tracks the position the potion is at before the animations start
        placedPos = transform.position;

        // Index 0 means the potion didn't land, and therefore, should start sliding back into place
        // Otherwise, it can do its pouring animations
        if (translater.objectIndex != 0)
        {
            StartCoroutine(PourPotion());
        }
        else
        {
            StartCoroutine(SlidePotionBack());
        }
    }

    // A Coroutine that animate the potion sliding back without the use of Update()
    public IEnumerator SlidePotionBack()
    {
        // Resets the time variable for the timer below
        time = 0;

        // The object the potion landed on will know to start its animations while the potion is being put away
        potionReturning.Invoke();

        // A 1.5 second timer
        while (time < 1.5f)
        {
            // time increases by deltaTime in order for the timer to actally finish in 1.5 seconds, instead of a random amount of frames
            time += Time.deltaTime;

            // potion lerps between the position it was placed at (placedPos), and the position it started the game at (startPos)
            transform.position = Vector3.Lerp(placedPos, startPos, curveSlide.Evaluate(time));

            // This stops the script for one frame, and continues it on the next
            // It lets this script run for multiple frames, instead of the animation all happening at once
            yield return null;
        }
        
    }

    // A Coroutine that is used to animate the potion being poured
    IEnumerator PourPotion()
    {
        // Resets the time variable for the timer below
        time = 0;

        // Grabs the position of the potion
        // transform.position can't have its values changed so easily,
        // so we need to take this extra step to change the x and y values seperatley
        Vector3 pos = transform.position;

        // Grabs the rotation of the potion
        // Like with transform.position, we save this variable to just rotate the z value
        Vector3 rot = transform.eulerAngles;

        // A 2 second timer
        while (time < 2)
        {
            // time increases by deltaTime in order for the timer to actally finish in 1.5 seconds, instead of a random amount of frames
            time += Time.deltaTime;

            // pos and rot get manipulated by their respective curves, before giving them to transform
            pos.x = placedPos.x + curveX.Evaluate(time);
            pos.y = placedPos.y + curveY.Evaluate(time);
            transform.position = pos;

            rot.z = curveR.Evaluate(time);
            transform.eulerAngles = rot;

            // This stops the script for one frame, and continues it on the next
            // It lets this script run for multiple frames, instead of the animation all happening at once
            yield return null;
        }
        // Once this animation is finished, the potion will start its sliding back animation
        StartCoroutine(SlidePotionBack());
    }

}