using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInteraction : MonoBehaviour
{
    public static ArduinoInteraction Instance { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void TurnOnLights()
    {

    }

    public float MatPressure()
    {
        return 0.0f;
    }
}
