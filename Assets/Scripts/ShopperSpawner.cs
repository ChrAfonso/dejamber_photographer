using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperSpawner : MonoBehaviour
{
    public GameObject[] ShopperPrefabs;
    public float Frequency = 5;
    public float FrequencyVariation = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnShopper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnShopper()
    {
        while(true) {
            yield return new WaitForSeconds(Random.Range(Frequency - FrequencyVariation, Frequency + FrequencyVariation));

            int randomIndex = Random.Range(0, ShopperPrefabs.Length);
            GameObject shopper = GameObject.Instantiate(ShopperPrefabs[randomIndex]);
            shopper.transform.parent = GameObject.Find("Shoppers").transform;
            
            float randomSpeed = Random.Range(2f, 5f);
            Vector2 directionSpeed = shopper.GetComponent<ShopperController>().Direction;
            shopper.GetComponent<ShopperController>().Direction = directionSpeed.normalized * 2;
        }
    }
}
