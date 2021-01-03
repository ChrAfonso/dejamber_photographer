using System.Collections.Generic;
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

    public string NeededItemName;

    public States playerState { get; private set; } = States.BORED;

    private GameObject homePosition;
    private Collider2D homeCollider;

    private SpriteRenderer highlight;
    
    private bool dragging = false;

    private Cooldown cooldown;
    private Mood moodCooldown; // optional
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
        moodCooldown = gameObject.GetComponent<Mood>();
        moveToTarget = gameObject.GetComponent<MoveToTarget>(); // TODO replace with generic moveToTarget
        highlight = gameObject.transform.Find("Highlight")?.GetComponent<SpriteRenderer>();

        // set random values for cooldowns
        cooldown.DurationCD = Random.Range(5, 7);
        if(moodCooldown) moodCooldown.MoodValue = Random.Range(8, 10);

        overlapsTriggers = new List<Collider2D>();

        EnterState(playerState);
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState) {
            case States.HAPPY:
                // TODO moodcooldown here
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
                    if(moveToTarget) {
                        EnterState(States.MOVING);
                    } else { // Grandparents
                        EnterState(States.SLEEP);
                    }
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
                // TODO start moodcooldown
                cooldown.enabled = true;
                cooldown.Reset();
                if(moveToTarget) moveToTarget.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = happySprite;
                break;

            case States.BORED:
                // start (move) cooldown, disable moodCooldown
                cooldown.enabled = true;
                cooldown.Reset();
                if(moveToTarget) moveToTarget.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = boredSprite;
                break;

            case States.MOVING:
                if(moveToTarget) {
                    moveToTarget.enabled = true;
                    moveToTarget.Reset();
                }
                cooldown.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = movingSprite;
                break;

            case States.ARRIVED:
                if(moveToTarget) moveToTarget.enabled = false;
                cooldown.enabled = false;

                if(spriteRenderer) spriteRenderer.sprite = arrivedSprite;
                break;

            case States.SLEEP:
                if(moveToTarget) moveToTarget.enabled = false;
                cooldown.enabled = false;

                if(!moveToTarget) {
                    // Grandparents
                    gameObject.GetComponent<AudioSource>()?.Play();
                }

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
        if(moveToTarget) moveToTarget.enabled = false;
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
            
            EnterState(States.BORED);
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

    void OnTriggerStay2D(Collider2D trigger) {
        if(!overlapsTriggers.Contains(trigger)) {
            Debug.Log("Adding trigger "+trigger.name+" on stay");
            overlapsTriggers.Add(trigger);
        }
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

    public void CheckItem(Collider2D itemCollider)
    {
        if(overlapsTriggers.Contains(itemCollider)) {
            if(NeededItemName == itemCollider.gameObject.name) {
                EnterState(States.HAPPY);
            }
        }
    }

    public void CheckItemHighlight(string itemName, bool show = true)
    {
        if(highlight) {
            if(NeededItemName.Equals(itemName) && this.playerState != States.HAPPY) {
                highlight.enabled = show;
            } else {
                highlight.enabled = false;
            }
        }
    }
}
