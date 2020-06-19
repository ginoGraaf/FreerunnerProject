using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoConnection : MonoBehaviour
{
    public int NumberOfNodes = 9;
    public bool[] ButtonStatus { get; private set; }
    public char[] LightStatus { get; private set; }
    public bool Connected { get; private set; } = false;

    private int ComPort;
    private SerialPort Arduino;
    private string SerialText = "";
    private readonly char[] delimiters = { '#', '%', '\n' };
    private IEnumerator deviceFinder;

    private char[] array = new char[5];

    // Connect to the serial port, setup the arrays at the proper lengths
    void Start()
    {
        deviceFinder = FindDevice();
        Connect();
        // Setup the Light array
        LightStatus = new char[NumberOfNodes];
        for(int i = 0; i < NumberOfNodes; i++)
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
        ReadSerialData();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        Arduino.Close();
    }

    public void Connect()
    {
        StopCoroutine(deviceFinder);
        StartCoroutine(deviceFinder);
    }
    public bool Connect(int comPort)
    {
        if(SetComPort(comPort))
        {
            Connected = true;
            return true;
        }
        return false;
    }

    private bool SetComPort(int comPort)
    {
        ComPort = comPort;
        Debug.Log(ComPort);
        Arduino = new SerialPort("COM" + comPort.ToString(), 9600);
        try
        {
            Arduino.Open();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
            return false;
        }
        Arduino.ReadTimeout = 1;
        if (Arduino.IsOpen)
        {
            Arduino.DiscardInBuffer();
            Arduino.DiscardOutBuffer();
        }
        return true;
    }

    IEnumerator FindDevice()
    {
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            SetComPort(int.Parse(port.Substring(3)));
            try
            {
                Arduino.Write("#REQ%");
                Arduino.BaseStream.Flush();
            }
            catch (InvalidOperationException)
            {
                //Serial port is not open, do nothing
            }

            yield return new WaitForSeconds(1.0f); // Give arduino time to receive and transmit

            if (Arduino.BytesToRead > 2)
            {
                try
                {
                    SerialText = Arduino.ReadLine();
                    
                    if (SplitSerial(SerialText) == "ACK")
                    {
                        Connected = true;
                        Debug.Log("CONNECTED");
                        StopCoroutine(deviceFinder);
                        break;
                    }
                }
                catch (TimeoutException)
                {
                    Debug.Log("TIMEOUT");
                }
                catch (InvalidOperationException)
                {
                    
                }
            }
        }
    }

    // Send data to the arduino
    void SendToArduino()
    {
        if(Connected)
        {
            string tempToArduino = "";
            for (int i = 0; i < NumberOfNodes; i++)
            {
                tempToArduino += LightStatus[i];
            }

            try
            {
                Arduino.Write("#" + tempToArduino + "%");
                Arduino.BaseStream.Flush();
            }
            catch(Exception)
            {
                Connected = false;
                Debug.Log("DISCONNECTED");
                StartCoroutine(deviceFinder);
            }

            Debug.Log("#" + tempToArduino + "%");
        }
    }

    // Set specific light to a color
    public void SetLight(int lightNumber, char color)
    {
        if(lightNumber < NumberOfNodes && lightNumber >= 0)
        {
            LightStatus[lightNumber] = color;
        }

        SendToArduino();
    }

    // Reads serial data from the serial port if there is data available
    void ReadSerialData()
    {
        if(Connected)
        {
            if (Arduino.BytesToRead > 2)
            {
                try
                {
                    SerialText = Arduino.ReadLine();
                    int ButtonNumberPressed = int.Parse(SplitSerial(SerialText));
                    for (int i = 0; i < NumberOfNodes; i++)
                    {
                        if (i == ButtonNumberPressed)
                        {
                            ButtonStatus[i] = true;
                            Debug.Log(i + "BUTTON PRESSED");
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
    }

    // Splits the string into a number of the button which was pressed
    string SplitSerial(string SerialText)
    {
        string[] split = SerialText.Split(delimiters);
        for (int i = 0; i < split.Length; i++)
        {
            if (split[i].Length > 0)
            {
                return split[i];
            }
        }
        return "";
    }
}
