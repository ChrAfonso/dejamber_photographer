using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckShoppersInPicture : MonoBehaviour
{
    private List<Collider2D> shoppersInPicture;
    private List<Collider2D> familyInPicture;

    // Start is called before the first frame update
    void Start()
    {
        shoppersInPicture = new List<Collider2D>();
        familyInPicture = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Shopper")
        {
            shoppersInPicture.Add(other);
            Debug.Log(shoppersInPicture.Count+" shopper in picture!");
        }
        else if(other.tag == "Person")
        {
            familyInPicture.Add(other);
            Debug.Log(familyInPicture.Count+" family members in picture!");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Shopper")
        {
            shoppersInPicture.Remove(other);
            Debug.Log(shoppersInPicture.Count+" shopper in picture!");
        }
        else if(other.tag == "Person")
        {
            familyInPicture.Remove(other);
            Debug.Log(familyInPicture.Count+" family members in picture!");
        }
    }

    public int CountShoppersInPicture()
    {
        return shoppersInPicture.Count;
    }

    public List<GameObject> FamilyInPicture()
    {
        List<GameObject> result = new List<GameObject>();
        for(int c = 0; c < familyInPicture.Count; c++) {
            result.Add(familyInPicture[c].gameObject);
        }
        return result;
    }
}
