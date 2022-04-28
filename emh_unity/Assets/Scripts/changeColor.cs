using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    Renderer rendererComponent;
    analogPinValues analogPinValues;

    // Start is called before the first frame update
    void Start()
    {
        rendererComponent = transform.GetComponent<Renderer>();
        analogPinValues = GameObject.Find("Arduino Nano").GetComponent<analogPinValues>();
    }

    // Update is called once per frame
    void Update()
    {
        if (analogPinValues.pulseState == true)
        {
            rendererComponent.material.color = new Color(1f, 0f, 0f);
        }
        else if (analogPinValues.pulseState == false)
        {
            rendererComponent.material.color = new Color(1f, 1f, 1f);
        }
    }
}
