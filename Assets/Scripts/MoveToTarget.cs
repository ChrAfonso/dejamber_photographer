using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public GameObject Target;
    public string TargetName;

    public float MinArriveDistance;
    public float MoveSpeed;

    public AudioClip SoundOnArrival;

    public bool arrived { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Target) {
            Debug.Log("Found target: " + Target.name);
        } else {
            Debug.Log("Could not find target!");
        }
    }

    public void Reset() {
        arrived = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target && !arrived) {
            Vector3 directionToTarget = Target.transform.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            if(distanceToTarget > MinArriveDistance) {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
            } else {
                Debug.Log("Arrived at target!");
                this.arrived = true;

                if(SoundOnArrival) {
                    gameObject.GetComponent<AudioSource>().PlayOneShot(SoundOnArrival);
                }
            }
        }
    }
}
