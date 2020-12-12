﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillCooldown : MonoBehaviour
{

    public float DurationCD = 5;
    private float leftTime = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTime < DurationCD)
        {
            leftTime += Time.deltaTime;
        }
        else
        {
           
            Debug.Log(" DurationCD ist wieder aufgefüllt.");
            
            this.enabled = false;
        }


    }
    public float getValue()
    {
        return leftTime;
    }
}
