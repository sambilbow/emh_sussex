using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using Freya;

public class analogPinValues : MonoBehaviour
{
    /* 
     * Create a new "SerialPort" connection called arduinoNanoStream
     * Define the serial port name and speed (Baud rate) to read the data stream at.
     * To find the port name, open Terminal (Mac) or Command Prompt (Windows) 
     * use the command "ls /dev/cu.*" (Mac) or "powershell", enter, then 
     * "[System.IO.Ports.SerialPort]::getportnames()" (Windows). Alternatively, on Windows
     * you can access Device Manager via right-clicking the Start Menu, and find the name 
     * under Ports (COM & LPT)
     * 
     * Mac serial port names: "/dev/cu.usbserial-1420"
     * Windows serial port names: "COM7" 
    */

    [Tooltip("Arduino Port")]
    public string serialPort = "/dev/tty.usbserial-1420";

    [Tooltip("Arduino Serial Baudrate")]
    public int baudRate = 115200; // Leave this as 115200

    private SerialPort arduinoNanoStream;


    // A couple of variables that help us parse the heart rate data over serial into three separate variables (Rate, Interval, Pulse). We use these variables in Update()
    private string arduinoNanoString;
    private string[] arduinoNanoList;

    // Variables for our heart rate data. "public" means they are displayed on this component in the Unity inspector 
    [Header("Heart Rate Sensor (A0)")]
    public int heartRate;
    public int heartInterval;
    public float heartPulse;
    public bool pulseState;

    // Display the variables holding our analog pin (1 - 7) data. Uncomment as needed 
    [Header("Analog Inputs (A1, A2, A3...)")]
    public float a1Value;
    public float a2Value;
    public float a3Value;
    //public float a4Value;
    //public float a5Value;
    //public float a6Value;
    //public float a7Value;


    // Start is called before the first frame update
    void Start()
    {

        /*
         * For context, the data stream coming through this port when no sensors are active will look something like this:
         * 
         * /a0/0,600,0        <-- Slightly special because the heart rate sensor gives us three readings
         * /a1/0
         * /a2/0
         * /a3/0
         * /a4/0
         * /a5/0
         * /a6/0
         * /a7/0
         * 
         * Essentially separating out each Analog Input's value by using a /a(n)/x identifier (where n = the input, x = the value)
        */

        // Configure our serial port variable
        arduinoNanoStream = new(serialPort, baudRate);
        arduinoNanoStream.ReadTimeout = 100;

        // Open up the serial port for incoming data stream
        arduinoNanoStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // Set this variable equal to the latest line of data from the Serial port stream (e.g. "/a0/0,600,0")
            arduinoNanoString = arduinoNanoStream.ReadLine();
            // Send this string to our OnMessageArrived Method to process it and extract our pin values.
            OnMessageArrived(arduinoNanoString);
        }
        catch (System.Exception error)
        {
            Debug.Log(error.Message);
        }

        //while (pulseState == true)
        //{
        //    transform.GetComponent<AudioSource>().Play();
        //}
    }

    void OnMessageArrived(string arduinoNanoString)
    {
        ////Debug.Log("Message received");
        ////Debug.Log(arduinoNanoString[2]);
        //if (arduinoNanoString[2].Equals('0'))
        //{
        //    Debug.Log(arduinoNanoString);
        //}
        // A switch/case statement which looks for "n" (the analog pin number), which is the 3rd (starting from 0) character in the line
        switch (arduinoNanoString[2])
        {
            // If n = 0 (e.g. /a0/x)
            case '0':
                // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                // Use the comma to split this line into a list of three values (Rate, Interval, Pulse)
                arduinoNanoList = arduinoNanoString.Split(',');
                // Set our Heart Rate variable from line 29 equal to the first variable in this list
                heartRate = int.Parse(arduinoNanoList[0]);
                // Set our Heart Interval variable from line 30 equal to the second variable in this list
                heartInterval = int.Parse(arduinoNanoList[1]);
                // Set our Heart Pulse variable from line 31 equal to a scaled version of the third variable in this list
                heartPulse = Mathf.Round(Mathfs.RemapClamped(0f, 600f, 0f, 1f, float.Parse(arduinoNanoList[2])));
                // Set our Pulse state variable
                pulseState = heartPulse > 0.99f;
                break;



            // If n = 1 (e.g. /a1/x)
            case '1':
                // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                a1Value = float.Parse(arduinoNanoString);
                break;

            // If n = 2 (e.g. /a2/x)
            case '2':
                // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                a2Value = float.Parse(arduinoNanoString);
                break;

            // If n = 3 (e.g. /a3/x)
            case '3':
                // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                arduinoNanoString = arduinoNanoString.Remove(0, 4);
                // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                a3Value = float.Parse(arduinoNanoString);
                break;









                // These have been commented, if you use more than 4 sensors (?!?!?!!!!) just uncomment them :) 

                //// If n = 4 (e.g. /a4/x)
                //case '4':
                //    // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
                //    // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                //    a4Value = float.Parse(arduinoNanoString);
                //    break;

                //// If n = 5 (e.g. /a5/x)    
                //case '5':
                //    // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
                //    // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                //    a5Value = float.Parse(arduinoNanoString);
                //    break;

                //// If n = 6 (e.g. /a6/x)
                //case '6':
                //    // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
                //    // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                //    a6Value = float.Parse(arduinoNanoString);
                //    break;

                //// If n = 7 (e.g. /a7/x)
                //case '7':
                //    // Remove the first 4 characters of the line (so that we're just left with the value of the pin)
                //    arduinoNanoString = arduinoNanoString.Remove(0, 4);
                //    // Convert the rest of this line into a decimal number, and set the variable visible in the inspector equal to the value from the data stream
                //    a7Value = float.Parse(arduinoNanoString);
                //    break;

        }
    }
}
