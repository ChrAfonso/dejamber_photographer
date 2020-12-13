using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickShopper : MonoBehaviour
{
    Vector3 Velocity;
    float rotation;

    bool flicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!flicked) {
            if(Input.GetMouseButtonDown(0)) {
                // Debug.Log("mousedown!");
                Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(worldRay);
                foreach (RaycastHit2D hit in hits)
                {
                    GameObject clickedObject = hit.transform.gameObject;
                    if(clickedObject == gameObject)
                    {
                        Velocity = new Vector3(Random.Range(2,5), Random.Range(2,5), Random.Range(0,4));
                        rotation = Random.Range(-180, 180);
                        gameObject.GetComponent<ShopperController>().Move = false;
                    }
                }
            }
        } else {
            transform.Translate(Velocity * Time.deltaTime);
            transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
        }
    }
}
