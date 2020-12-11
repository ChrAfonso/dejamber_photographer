using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    public float decreaseSpeed = 2f;
    private float currentValue = 100;
    private Cooldown cooldownScript;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        
        cooldownScript = gameObject.GetComponentInParent<Cooldown>();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = cooldownScript.getValue();
       
    }

    


}
