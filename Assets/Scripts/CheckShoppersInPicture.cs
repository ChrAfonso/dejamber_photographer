using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckShoppersInPicture : MonoBehaviour
{
    private List<Collider2D> shoppersInPicture;

    // Start is called before the first frame update
    void Start()
    {
        shoppersInPicture = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Shopper") {
            shoppersInPicture.Add(other);
            Debug.Log(shoppersInPicture.Count+" shopper in picture!");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Shopper") {
            shoppersInPicture.Remove(other);
            Debug.Log(shoppersInPicture.Count+" shopper in picture!");
        }
    }

    public int CountShoppersInPicture()
    {
        return shoppersInPicture.Count;
    }
}
