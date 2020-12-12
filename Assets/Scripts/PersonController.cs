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
        SLEEP
    }

    private States playerState = States.HAPPY;

    private bool dragging = false;

    private Cooldown cooldown;
    private Mood moodCooldown;
    private MoveToTarget moveToTarget;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = gameObject.GetComponent<Cooldown>();
        moodCooldown = gameObject.GetComponent<Mood>();
        moveToTarget = gameObject.GetComponent<MoveToTarget>();

        // set random values for cooldowns
        cooldown.DurationCD = Random.Range(5, 7);
        moodCooldown.MoodValue = Random.Range(8, 10);

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown.getValue() <= 0.5)
        {
            //visual notification for desire
            Debug.Log("need 0.5");
        }
        if (cooldown.getValue() == 0)
        {
            Debug.Log("Cooldown is zero");
            MonoBehaviour action = gameObject.GetComponent<MoveToBuffet>();
            action.enabled = true;
        }
        //Debug.Log(moodCooldown.getMood());
        if (moodCooldown.getMood() <= 0.5)
        {
            //bored state
            Debug.Log("bored 0.5");
            playerState = States.BORED;

        }
        if (moodCooldown.getMood() == 0)
        {
            Debug.Log("moodCooldown is zero");
            MonoBehaviour action = gameObject.GetComponent<MoveToBuffet>();
            action.enabled = true;
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

        // TODO check drop point - intersect with homePosition?

        // yes -> reset/snap to home position, idle state

        // no  -> move again to last visited target
        // moveToTarget.enabled = true;

    }
}
