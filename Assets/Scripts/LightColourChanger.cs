using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightColourChanger : MonoBehaviour
{
    public Color goColour;
    public Color stopColour;
    public Image img;
    public bool goOrStop = false;

    public void ColourSwitcher()
    {
        if (goOrStop)
        {
            img.color = goColour;
        } else
        {
            img.color = stopColour;
        }

        goOrStop = !goOrStop;
        img.color = goColour;
    }
}
