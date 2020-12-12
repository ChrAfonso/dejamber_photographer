using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperController : MonoBehaviour
{
    public Vector2 Direction;

    private bool onCamera = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Direction != null) {
            transform.Translate(Direction * Time.deltaTime);
        }

        // kill when offscreen
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(!onCamera) {
            if(viewportPosition.x > 0 && viewportPosition.x < 1) {
                onCamera = true;
            }
        } else {
            if(viewportPosition.x < 0 || viewportPosition.x > 1) {
                onCamera = false;
                GameObject.Destroy(gameObject);
            }
        }
    }
}
