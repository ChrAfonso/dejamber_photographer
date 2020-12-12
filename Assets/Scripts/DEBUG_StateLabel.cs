using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUG_StateLabel : MonoBehaviour
{
    private TextMeshPro textMesh;
    private PersonController personController;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        personController = gameObject.GetComponentInParent<PersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText(personController.playerState.ToString());
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        textMesh.color = color;
    }
}
