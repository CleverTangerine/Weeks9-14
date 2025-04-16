using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicSpawner : MonoBehaviour
{
    // Reference to Assets prefabs can't reference on their own
    public PotionTranslater potionTranslater;

    // The Source is the actual prefab
    public GameObject potionSource;

    // The Array is the list of prefabs
    public GameObject[] potionArray;

    // The Sprite is the list of images this prefab will use
    public Sprite[] potionSprite;
    // Potion sprite credit at <a href="https://www.flaticon.com/free-icons/potion" title="potion icons">Potion icons created by Freepik - Flaticon</a>

    // Read above for the purpose of the Source, Array, and Sprites
    public GameObject objectSource;
    public GameObject[] objectArray;
    public Sprite[] objectSprite;

    // The Canvas reference is used to make spawned prefabs children of the Canvas
    // Otherwise, the prefabs would be Instantiated outside of the Canvas, and wouldn't function properly
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the size of the Arrays
        potionArray = new GameObject[4];
        objectArray = new GameObject[5];
        // These for loops create the potion/object prefabs
        // The reason the "i" starts at one is because index 0 indicates the player either isn't holding a potion, or didn't drop a potion on an object
        for (int i = 1; i <= 3; i++)
        {
            MakePotion(i);
        }
        // You might think that drawing the potion first is dumb because then you can't see it
        // However, spawning them that way makes PointerEnter not work
        // This cost me 12 hours of debugging, I'm not kidding
        for (int i = 1; i <= 4; i++)
        {
            MakeObject(i);
        }
    }
    void MakePotion(int index)
    {
        // Prefab is instantiated, and then added to the Array to be referenced later by other scripts      
        GameObject newPotion = Instantiate(potionSource);
        potionArray[index] = newPotion;

        // Setting the parent as the Canvas makes it spawn properly as UI, and not gameObject in worldspace
        newPotion.transform.parent = canvas.transform;

        // Setting the position/scale/sprite in relation to the prefabs index
        // This intuitively makes them all spawn in unique positions with unique sprites
        newPotion.transform.position = new Vector2(-3 * index, 3);
        newPotion.transform.localScale = Vector2.one * 40;
        newPotion.GetComponent<Image>().sprite = potionSprite[index - 1];


        // Getting a reference to the prefabs object script, in order to inject some values we can't apply from the editor
        PotionScript potionScript = newPotion.GetComponent<PotionScript>();

        // We tell the object its index so the prefab can recognize it's index in the objectArray
        potionScript.potionType = index;

        // Prefabs can't get references to other scripts if it hasn't been instantiated in the Editor yet, so they're referenced here
        potionScript.translater = potionTranslater;
    }

    void MakeObject(int index)
    {
        // Prefab is instantiated, and then added to the Array to be referenced later by other scripts
        GameObject newObject = Instantiate(objectSource);
        objectArray[index] = newObject;

        // Setting the parent as the Canvas makes it spawn properly as UI, and not gameObject in worldspace
        newObject.transform.parent = canvas.transform;

        // Setting the position/scale/sprite in relation to the prefabs index
        // This intuitively makes them all spawn in unique positions with unique sprites
        newObject.transform.position = new Vector2(4 * (index - 2.5f), -3);
        newObject.transform.localScale = Vector2.one * 80;
        Debug.Log(index);
        

        // Getting a reference to the prefabs object script, in order to inject some values we can't apply from the editor
        ObjectScript objectScript = newObject.GetComponent<ObjectScript>();
        // We tell the object its index so the prefab can recognize it's index in the objectArray
        objectScript.objectType = index;

        // Prefabs can't get references to other scripts if it hasn't been instantiated in the Editor yet, so they're referenced here
        objectScript.translater = potionTranslater;
        objectScript.spawner = this;
        newObject.GetComponent<Image>().sprite = objectSprite[index - 1];
    }

    
}
