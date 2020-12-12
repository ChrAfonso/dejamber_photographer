using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public string ActionAtEnd = "";
    public float DurationCD = 5;
    private float leftTime = 0;

    


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void Reset() {
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
            Debug.Log("Ist angekommen. - Cooldown finished");
            MonoBehaviour action = gameObject.GetComponent<MoveToBuffet>();
            action.enabled = true;
            this.enabled = false;

        }

        
    }
    public float getValue()
    {
        return leftTime / DurationCD;
    }

   
}
