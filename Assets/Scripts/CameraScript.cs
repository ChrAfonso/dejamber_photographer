using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using JetBrains.Annotations;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public int counter = 0;
    
    public Texture2D screenshot1;
    public Texture2D screenshot2;
    public Texture2D screenshot3;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mousedown!");
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(worldRay);
            foreach (RaycastHit2D hit in hits)
            {
                counter++;
                if (counter == 1)
                {
                    Debug.Log("screenshot1");
                    screenshot1 = SC_ScreenAPI.CaptureScreen();
                }
                if (counter == 2)
                {
                    Debug.Log("screenshot2");
                    screenshot2 = SC_ScreenAPI.CaptureScreen();
                }
                if (counter == 3)
                {
                    Debug.Log("screenshot3");
                    screenshot3 = SC_ScreenAPI.CaptureScreen();

                    //game over method in gamecontroller
                }
            }
        }
    }
}
