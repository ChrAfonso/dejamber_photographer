using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBuffet : MonoBehaviour
{
    GameObject buffet;

    public float MinArriveDistance;
    public float MoveSpeed;

    public bool arrived { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        buffet = GameObject.FindGameObjectWithTag("Buffet");
        if(buffet) {
            Debug.Log("Found buffet: " + buffet.name);
        } else {
            Debug.Log("Could not find buffet!");
        }
    }

    public void Reset() {
        arrived = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(buffet && !arrived) {
            Vector3 directionToBuffet = buffet.transform.position - transform.position;
            float distanceToBuffet = directionToBuffet.magnitude;
            // Debug.Log("Distance to buffet: " + distanceToBuffet);
            if(distanceToBuffet > MinArriveDistance) {
                transform.position = Vector3.MoveTowards(transform.position, buffet.transform.position, MoveSpeed * Time.deltaTime);
            } else {
                Debug.Log("Arrived at buffet!");
                this.arrived = true;
            }
        }
    }
}
