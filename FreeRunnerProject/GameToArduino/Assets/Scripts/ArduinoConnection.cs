using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class ArduinoConnection : MonoBehaviour
{
    public Dropdown dropdownCom;
    public Dropdown dropdownNumberOfNodes;

    public int NumberOfNodes;
    public int ComPort;
    public bool[] ButtonStatus { get; private set; }
    public char[] LightStatus { get; private set; }

    private SerialPort Arduino;
    private string SerialText = "";
    private readonly char[] delimitors = { '#', '%', '\n' };

    // Connect to the serial port, setup the arrays at the proper lengths
    void Start()
    {
        ComPort = int.Parse(dropdownCom.captionText.text);
        NumberOfNodes = int.Parse(dropdownNumberOfNodes.captionText.text);

        Debug.Log(ComPort);
        Debug.Log(NumberOfNodes);

        // Setup the Arduino connection
        Arduino = new SerialPort("COM" + ComPort.ToString(), 9600);
        try
        {
            Arduino.Open();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
        }
        //Arduino.Open();
        Arduino.ReadTimeout = 1;
        if (Arduino.IsOpen)
        {
            Arduino.DiscardInBuffer();
            Arduino.DiscardOutBuffer();
        }

        // Setup the Light array
        LightStatus = new char[NumberOfNodes];
        for (int i = 0; i < NumberOfNodes; i++)
        {
            LightStatus[i] = '0';
        }

        // Setup the button array
        ButtonStatus = new bool[NumberOfNodes];
        for (int i = 0; i < NumberOfNodes; i++)
        {
            ButtonStatus[i] = false;
        }
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            ReadSerialData();
        }
        catch(InvalidOperationException)
        {
            Debug.Log("InvalidOperationException: ");
        }

        //ReadSerialData();
    }

    // Send data to the arduino
    void SendToArduino()
    {
        string tempToArduino = "";
        for (int i = 0; i < NumberOfNodes; i++)
        {
            tempToArduino += LightStatus[i];
        }
        Arduino.Write("#" + tempToArduino + "%");
        Arduino.BaseStream.Flush();
        Debug.Log("#" + tempToArduino + "%");
    }

    // Set specific light to a color
    public void SetLight(int lightNumber, char color)
    {
        if (lightNumber < NumberOfNodes && lightNumber >= 0)
        {
            LightStatus[lightNumber] = color;
        }

        SendToArduino();
    }

    // Reads serial data from the serial port if there is data available
    void ReadSerialData()
    {
        if (Arduino.BytesToRead > 2)
        {
            try
            {
                SerialText = Arduino.ReadLine();
                int ButtonNumberPressed = SplitSerial(SerialText);
                for (int i = 0; i < NumberOfNodes; i++)
                {
                    if (i == ButtonNumberPressed)
                    {
                        ButtonStatus[i] = true;
                        Debug.Log(i);
                    }
                    else
                    {
                        ButtonStatus[i] = false;
                    }
                }
            }
            catch (TimeoutException) { }
        }
    }

    // Splits the string into a number of the button which was pressed
    int SplitSerial(string SerialText)
    {
        string[] split = SerialText.Split(delimitors);
        for (int i = 0; i < split.Length; i++)
        {
            if (split[i].Length > 0)
            {
                return int.Parse(split[i]);
            }
        }
        return -1;
    }
}
