﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PictureFrame;

    public delegate void SaveScreenhot(Texture2D screenshot);
    SaveScreenhot saveScreenshot;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(worldRay);
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject == gameObject) {
                    Debug.Log("screenshot!");
                    Texture2D screenshot = SC_ScreenAPI.CaptureScreen();

                    GameController.instance.SaveScreenshot(screenshot);
                }
            }
        }
    }
}
