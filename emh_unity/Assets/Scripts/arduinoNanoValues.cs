using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using Freya;

public class arduinoNanoValues : MonoBehaviour
{
    SerialPort arduinoNanoStream = new SerialPort("COM7", 115200);


    [Header("Heart Rate Sensor (A0)")]
    public int heartRate;
    public int heartInterval;
    public float heartPulse;
    private string arduinoNanoString;
    private string[] arduinoNanoList;
    [Header("Analog Inputs (A1 - A7)")]
    public float a1Value;
    public float a2Value;
    public float a3Value;
    public float a4Value;
    public float a5Value;
    public float a6Value;
    public float a7Value;


    // Start is called before the first frame update
    void Start()
    {
        arduinoNanoStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        arduinoNanoString = arduinoNanoStream.ReadLine();
        switch (arduinoNanoString[2])
        {
            case '0':
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                arduinoNanoList = arduinoNanoString.Split(',');
                heartRate = int.Parse(arduinoNanoList[0]);
                heartInterval = int.Parse(arduinoNanoList[1]);
                heartPulse = float.Parse(arduinoNanoList[2]);
                break;
            case '1':
                Debug.Log(arduinoNanoString);
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                a1Value = float.Parse(arduinoNanoString);
                break;
            case '2':
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                a2Value = float.Parse(arduinoNanoString);
                break;
            case '3':
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                a3Value = float.Parse(arduinoNanoString);
                break;
            case '4':
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                a4Value = float.Parse(arduinoNanoString);
                break;
            //case '5':
            //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
            //    a5Value = float.Parse(arduinoNanoString);
            //    break;
            //case '6':
            //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
            //    a6Value = float.Parse(arduinoNanoString);
            //    break;
            //case '7':
            //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
            //    a7Value = float.Parse(arduinoNanoString);
            //    break;

        }
    }
}
