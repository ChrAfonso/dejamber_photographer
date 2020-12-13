using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float TotalTime = 120;

    private float time;
    private float sec;
    private float min;

    private void Start()
    {
        if(!textMesh) textMesh = gameObject.GetComponent<TextMeshPro>();
    }

    public void Reset()
    {
        time = TotalTime;

        StartCoroutine("StopWatch");
    }

    IEnumerator StopWatch()
    {
        while (true)
        {
            
            time -= Time.deltaTime;
            sec = (int)time % 60;
            min = (int)(time / 60 % 60);

            textMesh.text = string.Format("{0:00}:{1:00}", min, sec);

            yield return null;
        }
    }
}
