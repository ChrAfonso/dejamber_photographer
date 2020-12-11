using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillCooldown : MonoBehaviour
{
    public string ActionAtEnd = "";
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
            leftTime = DurationCD;
            Debug.Log("Ist wieder aufgefüllt. - Cooldown finished");
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
