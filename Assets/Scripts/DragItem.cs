using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private GameObject currentDraggingItem;

    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mousedown!");
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(worldRay);
            if (hit)
            {
                GameObject clickedObject = hit.transform.gameObject;
                // Debug.Log("hit object with tag: "+clickedObject.tag);
                if (clickedObject == gameObject)
                {
                    Debug.Log("currentDraggingItem: " + gameObject.name);
                    currentDraggingItem = clickedObject;

                    // highlight associated person
                    // HACKY
                    foreach(GameObject person in GameObject.FindGameObjectsWithTag("Person")) {
                        person.GetComponent<PersonController>().CheckItemHighlight(gameObject.name);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouseup!");
            if (currentDraggingItem)
            {
              currentDraggingItem = null;
              transform.position = spawnPosition; // reset
              
              // HACKY
              foreach(GameObject person in GameObject.FindGameObjectsWithTag("Person")) {
                  person.GetComponent<PersonController>().CheckItemHighlight(gameObject.name, false);
                  person.GetComponent<PersonController>().CheckItem(gameObject.GetComponent<Collider2D>());
              }
            }
        }

        if (currentDraggingItem)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // otherwise we always get same point at camera (which is at -10)
            // Debug.Log("mousePosition on screen: "+Input.mousePosition);

            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            // Debug.Log("mousePosition in world: "+mousePositionInWorld);

            currentDraggingItem.transform.position = mousePositionInWorld;
        }
    }
}
