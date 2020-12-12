using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// Control state of person: trigger happy/nervous states, start cooldown/move, reset to home position, show item desire

public class PersonController : MonoBehaviour
{
    private enum States
    {
        HAPPY,
        BORED,
        MOVING,
        SLEEP
    }

    public GameObject HomePosition;
    
    private States playerState = States.HAPPY;

    private bool dragging = false;

    private Cooldown cooldown;
    private Mood moodCooldown;
    private MoveToBuffet moveToTarget;

    new private Collider2D collider;

    private List<Collider2D> overlapsTriggers;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();

        cooldown = gameObject.GetComponent<Cooldown>();
        // moodCooldown = gameObject.GetComponent<Mood>();
        moveToTarget = gameObject.GetComponent<MoveToBuffet>(); // TODO replace with generic moveToTarget

        // set random values for cooldowns
        cooldown.DurationCD = Random.Range(5, 7);
        // moodCooldown.MoodValue = Random.Range(8, 10);

        overlapsTriggers = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState) {
            case States.HAPPY:
                if(!cooldown.enabled) {
                    cooldown.enabled = true;
                    cooldown.Reset();
                }

                if (cooldown.getValue() <= 0.5)
                {
                    // TODO visual notification for desire
                    Debug.Log("need 0.5");
                    playerState = States.BORED;
                }
                break;

            case States.BORED:
                if (cooldown.getValue() == 0)
                {
                    Debug.Log("Cooldown is zero");
                    playerState = States.MOVING;
                }
                break;

            case States.MOVING:
                if(!moveToTarget.enabled) {
                    moveToTarget.enabled = true;
                    moveToTarget.Reset();
                }

                if(moveToTarget.arrived) {
                    Debug.Log("Arrived!");
                    // playerState = States.ARRIVED;
                }
                break;
            
            case States.SLEEP:
                // TODO stay until click
                break;
        }

    }

    public void StartDragging()
    {
        dragging = true;

        // TODO store state
        cooldown.enabled = false;
        moodCooldown.enabled = false;
        moveToTarget.enabled = false;
    }

    public void StopDragging()
    {
        dragging = false;

        // check drop point - intersect with homePosition?
        bool droppedOnHome = false;
        if(HomePosition) {
            Collider2D HomePositionCollider = HomePosition.GetComponent<Collider2D>();
            if(HomePositionCollider && HomePositionCollider.IsTouching(collider)) {
                Debug.Log("Dropped person "+gameObject.name+" on home!");
                droppedOnHome = true;
            }
        }
        
        //HACK
        droppedOnHome = true;
        if(droppedOnHome) {
            // yes -> reset/snap to home position, idle state

            transform.position = HomePosition.transform.position;

            playerState = States.HAPPY;
        }
        else
        {
            // no  -> move again to last visited target
            moveToTarget.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        Debug.Log("Enter trigger "+trigger.name);
        overlapsTriggers.Add(trigger);
    }

     void OnTriggerExit2D(Collider2D trigger) {
        Debug.Log("Exit trigger "+trigger.name);
        overlapsTriggers.Remove(trigger);
    }
}
