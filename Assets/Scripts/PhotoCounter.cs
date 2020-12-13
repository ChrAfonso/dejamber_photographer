using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCounter : MonoBehaviour
{
    private int showIcons = 3;
    private GameObject[] photoIcons;

    // Start is called before the first frame update
    void Start()
    {
        photoIcons = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++) {
            photoIcons[i] = transform.GetChild(i).gameObject;
        }
    }

    public void ShowIcons(int number)
    {
        if(photoIcons == null) {
            return; // not initialized yet
        }

        if(number > 3) number = 3;
        if(number < 1) number = 1;

        for(int i = 0; i < photoIcons.Length; i++) {
            if(i < number) {
                photoIcons[i].SetActive(true);
            } else {
                photoIcons[i].SetActive(false);
            }
        }
    }
}
