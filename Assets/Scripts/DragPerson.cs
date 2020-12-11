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
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(worldRay);
            if (hit)
            {
                GameObject clickedObject = hit.transform.gameObject;
                if (clickedObject.tag == "Person") 
                {
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
            }
        }
    }
}
