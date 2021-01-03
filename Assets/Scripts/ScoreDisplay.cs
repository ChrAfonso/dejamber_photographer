using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshPro textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float score = 0;
        GameController.PhotoScore bestScore = GameController.instance.GetBestScore();
        if(bestScore != null) score = bestScore.score; 
        textMesh.text = string.Format("{0}", score);
    }
}
