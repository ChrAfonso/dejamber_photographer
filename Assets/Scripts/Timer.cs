using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float time;
    public float sec;
    public float min;



    private void Start()
    {
        StartCoroutine("StopWatch");
    }

    IEnumerator StopWatch()
    {
        time = 120;

        while (true)
        {
            
            time -= Time.deltaTime;
            sec = (int)time % 60;
            min = (int)(time / 60 % 60);

            timer.text = string.Format("{0:00}:{1:00}", min, sec);

            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
