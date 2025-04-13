using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 6; i++)
        {
            MakePotion(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakePotion(int potionID)
    {
        GameObject newPotion = Instantiate(prefab, Random.insideUnitCircle * 4, transform.rotation);
        // We want the potion prefab to listen to any of the objects. It might be good if the translator is used.
    }
}
