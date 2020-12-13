using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBestPicture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.PhotoScore bestScore = GameController.instance.GetBestScore();
        GetComponent<MeshRenderer>().material.mainTexture = bestScore.photo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
