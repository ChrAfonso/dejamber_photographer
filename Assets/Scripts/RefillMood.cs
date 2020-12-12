using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillMood : MonoBehaviour
{
    public float MoodValue;
    private float leftMood;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (leftMood < MoodValue)
        {
            leftMood += Time.deltaTime;
        }
        else
        {
            Debug.Log(" MoodValue ist wieder aufgefüllt.");
            
            this.enabled = false;
        }


    }
    public float getValue()
    {
        return leftMood;
    }
}
