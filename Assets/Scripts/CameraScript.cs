using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PictureFrame;
    public float Delay = 3;

    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(worldRay);
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject == gameObject) {
                    Debug.Log("Say cheeeeeeese!");
                    // TODO Sound

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
            Vector2 topleft = Camera.main.WorldToScreenPoint(frameBounds.min);
            Vector2 size = Camera.main.WorldToScreenPoint(frameBounds.size);
            bounds.Set(topleft.x, topleft.y, size.x, size.y);
        }
        Texture2D screenshot = SC_ScreenAPI.CaptureScreen(bounds);

        GameController.instance.SaveScreenshot(screenshot);
    }
}
