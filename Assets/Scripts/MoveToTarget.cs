using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public GameObject Target;

    public float MinArriveDistance;
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if(!Target) {
            Debug.Log("Found target: " + Target.name);
        } else {
            Debug.Log("Could not find target!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Target) {
            Vector3 directionToBuffet = Target.transform.position - transform.position;
            float distanceToTarget = directionToBuffet.magnitude;
            Debug.Log("Distance to target: " + distanceToTarget);

            // TODO: replace with collider/trigger check
            if(distanceToTarget > MinArriveDistance) {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
            } else {
                Debug.Log("Arrived at buffet!");

                // TODO

                this.enabled = false;
            }
        }
    }
}
