using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control state of person: trigger happy/nervous states, start cooldown/move, reset to home position, show item desire

public class PersonController : MonoBehaviour
{
    private bool dragging = false;

    private Cooldown cooldown;
    private MoveToTarget moveToTarget;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = gameObject.GetComponent<Cooldown>();
        moveToTarget = gameObject.GetComponent<MoveToTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDragging()
    {
        dragging = true;

        // TODO store state
        cooldown.enabled = false;
        moveToTarget.enabled = false;
    }

    public void StopDragging()
    {
        dragging = false;

        // TODO check drop point - intersect with homePosition?
        
        // yes -> reset/snap to home position, idle state
        
        // no  -> move again to last visited target
        // moveToTarget.enabled = true;
    }
}
