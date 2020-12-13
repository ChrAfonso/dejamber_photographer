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
                        Velocity = new Vector3(Random.Range(5,10), Random.Range(5,10), Random.Range(2,10));
                        rotation = Random.Range(-720, 720);
                        gameObject.GetComponent<ShopperController>().Move = false;
                        flicked = true;
                        StartCoroutine("Kill");
                    }
                }
            }
        } else {
            transform.Translate(Velocity * Time.deltaTime);
            transform.Rotate(transform.position + Vector3.forward, rotation * Time.deltaTime);
        }
    }

    IEnumerator Kill() {
        yield return new WaitForSeconds(Random.Range(2, 4));

        GameObject.Destroy(gameObject);
    }
}
