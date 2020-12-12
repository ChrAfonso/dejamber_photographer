using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public string ActionAtEnd = "";
    public float DurationCD;
    private float leftTime;

    


    // Start is called before the first frame update
    void Start()
    {
        leftTime = DurationCD;
        
}

    // Update is called once per frame
    void Update()
    {


        if (leftTime>0)
        {
            leftTime -= Time.deltaTime;
        }
        else
        {
            leftTime = 0;
            this.enabled = false;

        }

        
    }
    public float getValue()
    {
        return leftTime / DurationCD;
    }

   
}
