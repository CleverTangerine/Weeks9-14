using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    public GameObject[] potionList;
    public GameObject[] objectList;

    // Start is called before the first frame update
    void Start()
    {
        // These for loops create the potion/object prefabs
        // The reason the "i" starts at one is because index 0 indicates the player either isn't holding a potion, or didn't drop a potion on an object
        for (int i = 1; i <= 6; i++)
        {
            MakePotion(i);
        }
        for (int i = 1; i <= 4; i++)
        {
            MakeObject(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakePotion(int index)
    {
        // pos is used to get the position of the spawned item
        // The x position is multiplied by index to make each potion away from eachother
        Vector3 pos = new Vector3(index * 100, 100, 0);
        GameObject newPotion = Instantiate(potionList[index], pos, transform.rotation);
        // We're getting the potionScript the script so we can pass down the index
        PotionScript potionScript = newPotion.GetComponent<PotionScript>();
        potionScript.potionType = index;
        potionScript.originalPos = pos;
    }

    void MakeObject(int index)
    {
        GameObject newObject = Instantiate(objectList[index], Random.insideUnitCircle * 4, transform.rotation);
        ObjectScript objectScript = newObject.GetComponent<ObjectScript>();
        objectScript.objectType = index;
        // We want the potion prefab to listen to any of the objects. It might be good if the translator is used.
    }
}
