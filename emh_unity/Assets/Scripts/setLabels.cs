using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setLabels : MonoBehaviour
{

    // Create a variable for our Arduino values script
    analogPinValues analogPinValues;

    // Create variables for our labels that point to their TextMeshPro component
    public TextMeshPro heartRateLabel;
    public TextMeshPro heartIntervalLabel;
    public TextMeshPro heartPulseLabel;
    public TextMeshPro analogPin1Label;
    public TextMeshPro analogPin2Label;


    // Start is called before the first frame update
    void Start()
    {
        // Set our Arduino values script variable by finding the object's component in the hierarchy
        analogPinValues = GameObject.Find("Arduino Nano").GetComponent<analogPinValues>();
    }

    // Update is called once per frame
    void Update()
    {

        // Set our label's texts equal to the values from the Arduino
        heartRateLabel.text = "Heart Rate (BPM):          " + analogPinValues.heartRate.ToString();
        heartIntervalLabel.text = "Heart Interval (ms):        " + analogPinValues.heartInterval.ToString();
        heartPulseLabel.text = "Heart Pulse (0-1):           " + analogPinValues.heartPulse.ToString();
        analogPin1Label.text = "Analog Pin 1 (0-1024):   " + analogPinValues.a1Value.ToString();
        analogPin2Label.text = "Analog Pin 2 (0-1024):   " + analogPinValues.a2Value.ToString();
    }
}
