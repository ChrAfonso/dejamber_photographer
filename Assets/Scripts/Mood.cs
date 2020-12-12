using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mood : MonoBehaviour
{

    public float MoodValue;
    private float leftMood;

    // Start is called before the first frame update
    void Start()
    {
        leftMood = MoodValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (leftMood > 0)
        {
            leftMood -= Time.deltaTime;
        }
    }

    public float getMood()
    {
        return leftMood / MoodValue;
    }
}
