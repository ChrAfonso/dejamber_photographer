using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// Control state of person: trigger happy/nervous states, start cooldown/move, reset to home position, show item desire

public class PersonController : MonoBehaviour
{
    public enum States
    {
        HAPPY,
        BORED,
        MOVING,
        ARRIVED,
        SLEEP
    }

    public Sprite happySprite;
    public Sprite boredSprite;
    public Sprite sleepSprite;
    public Sprite movingSprite;
    public Sprite arrivedSprite;

    public States playerState { get; private set; } = States.HAPPY;

    private GameObject homePosition;
    private Collider2D homeCollider;
    
    private bool dragging = false;

    private Cooldown cooldown;
    private Mood moodCooldown;
    private MoveToTarget moveToTarget;

    new private Collider2D collider;

    private SpriteRenderer spriteRenderer;

    private List<Collider2D> overlapsTriggers;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // Sprite is in child object

        cooldown = gameObject.GetComponent<Cooldown>();
        // moodCooldown = gameObject.GetComponent<Mood>();
        moveToTarget = gameObject.GetComponent<MoveToTarget>(); // TODO replace with generic moveToTarget

        // set random values for cooldowns
        cooldown.DurationCD = Random.Range(5, 7);
        // moodCooldown.MoodValue = Random.Range(8, 10);

        overlapsTriggers = new List<Collider2D>();

        EnterState(States.HAPPY);
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState) {
            case States.HAPPY:
                if (cooldown.getValue() <= 0.5)
                {
                    // TODO visual notification for desire
                    Debug.Log("need 0.5");
                    EnterState(States.BORED);
                }
                break;

            case States.BORED:
                if (cooldown.getValue() == 0)
                {
                    Debug.Log("Cooldown is zero");
                    EnterState(States.MOVING);
                }
                break;

            case States.MOVING:
                if(moveToTarget.arrived) {
                    Debug.Log("Arrived!");
                    EnterState(States.ARRIVED);
                }
                break;
            
            case States.ARRIVED:
                // TODO do nothing, eat/reload?
                break;

            case States.SLEEP:
                // TODO stay until click
                break;
        }

    }

    private void EnterState(States newState) {
        switch(newState) {
            case States.HAPPY:
                cooldown.enabled = true;
                cooldown.Reset();
                moveToTarget.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = happySprite;
                break;

            case States.BORED:
                moveToTarget.enabled = false;
                cooldown.enabled = true;

                if(spriteRenderer) spriteRenderer.sprite = boredSprite;
                break;

            case States.MOVING:
                moveToTarget.enabled = true;
                moveToTarget.Reset();
                cooldown.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = movingSprite;
                break;

            case States.ARRIVED:
                moveToTarget.enabled = false;
                cooldown.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = arrivedSprite;
                break;

            case States.SLEEP:
                moveToTarget.enabled = false;
                cooldown.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = sleepSprite;
                break;
        }

        playerState = newState;
    }

    public void StartDragging()
    {
        dragging = true;
        homePosition.GetComponentInChildren<SpriteRenderer>().enabled = true;

        // TODO store state, if adding specific dragging state (flailing arms, sound, ...?)
        cooldown.enabled = false;
        if(moodCooldown != null) moodCooldown.enabled = false;
        moveToTarget.enabled = false;
    }

    public void StopDragging()
    {
        dragging = false;
        homePosition.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // check drop point - intersect with homePosition?
        bool droppedOnHome = overlapsTriggers.Contains(homeCollider);
        
        if(droppedOnHome) {
            Debug.Log("Dropped on home!");

            // yes -> reset/snap to home position, idle state
            
            transform.position = homePosition.transform.position;
            
            EnterState(States.HAPPY);
        }
        else
        {
            // no -> re-enter current state (and activate relevant scripts)
            EnterState(playerState);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        Debug.Log("Enter trigger "+trigger.name);
        overlapsTriggers.Add(trigger);
        Debug.Log(overlapsTriggers.Count);
    }

     void OnTriggerExit2D(Collider2D trigger) {
        Debug.Log("Exit trigger "+trigger.name);
        overlapsTriggers.Remove(trigger);
        Debug.Log(overlapsTriggers.Count);
    }

    public void SetHomePosition(GameObject homePosition)
    {
        this.homePosition = homePosition;
        this.homeCollider = homePosition.GetComponent<Collider2D>();
    }
}
