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
    public void SetColor(string color)
    {
        
        Debug.Log("Pulse Detected, Changing Color");
        ColorUtility.TryParseHtmlString(color, out Color convertedColor);
        rendererComponent.material.color = convertedColor;
    }
}
