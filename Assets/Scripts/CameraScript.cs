using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PictureFrame;
    public float Delay = 2;
    bool lockCamera = false;

    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    void Update()
    {
        if (!lockCamera && Input.GetMouseButtonDown(0))
        {
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(worldRay);
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject == gameObject) {
                    GetComponent<AudioSource>().Play();

                    lockCamera = true;
                    StartCoroutine("TakePhoto");
                }
            }
        }
    }

    IEnumerator TakePhoto()
    {
        yield return new WaitForSeconds(Delay);
        yield return frameEnd;

        Debug.Log("Snap!");

        Rect bounds = Rect.zero;
        if(PictureFrame) {
            Bounds frameBounds = PictureFrame.GetComponent<Renderer>().bounds;
            bounds = BoundsToScreenRect(frameBounds);
        }
        Texture2D screenshot = SC_ScreenAPI.CaptureScreen(bounds);

        GameController.instance.SaveScreenshot(screenshot);
        lockCamera = false;
    }

    public Rect BoundsToScreenRect(Bounds bounds)
    {
        // Get mesh origin and farthest extent (this works best with simple convex meshes)
        Vector3 origin = Camera.main.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, 0f));
        Vector3 extent = Camera.main.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, 0f));
        Debug.Log("screen bounds: " + origin + " - " + extent);
        
        // origin = Camera.main.WorldToViewportPoint(new Vector3(bounds.min.x, bounds.max.y, 0f));
        // extent = Camera.main.WorldToViewportPoint(new Vector3(bounds.max.x, bounds.min.y, 0f));
        // Debug.Log("vp bounds: " + origin + " - " + extent);
        
        // Create rect in screen space and return - does not account for camera perspective
        // HACK: currently disregarded for hard values!
        return new Rect(origin.x, Screen.height - origin.y, extent.x - origin.x, origin.y - extent.y);
    }
}
