using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPerson : MonoBehaviour
{
    private GameObject currentDraggingPerson; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            // Debug.Log("mousedown!");
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(worldRay);
            foreach (RaycastHit2D hit in hits)
            {
                GameObject clickedObject = hit.transform.gameObject;
                if(clickedObject == gameObject)
                {
                    Debug.Log("currentDraggingPerson: "+gameObject.name);
                    currentDraggingPerson = clickedObject;

                    PersonController personController = currentDraggingPerson.GetComponent<PersonController>();
                    if (personController)
                    {
                        personController.StartDragging();
                        Debug.Log("Picking up char "+currentDraggingPerson.name+"!");
                    }
                }
            }
        } else if(Input.GetMouseButtonUp(0)) {
            if(currentDraggingPerson) {
                PersonController personController = currentDraggingPerson.GetComponent<PersonController>();
                if (personController)
                {
                    personController.StopDragging();
                    Debug.Log("Dropping char "+currentDraggingPerson.name+"!");
                }

                currentDraggingPerson = null;
            }
        }

        if(currentDraggingPerson) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // otherwise we always get same point at camera (which is at -10)

            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);

            currentDraggingPerson.transform.position = mousePositionInWorld;
        }
    }
}
