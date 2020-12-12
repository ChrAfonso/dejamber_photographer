using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperController : MonoBehaviour
{
    public Vector2 Direction;

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

        // TODO: kill when offscreen
    }
}
