using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLight : MonoBehaviour
{
    [SerializeField] private GameObject Lamp;
    [HideInInspector] public enum state {ON,OFF,DANGER, BLINK};
    state lightState = state.OFF;
    private Material lampMaterial;

    void Awake()
    {
        lampMaterial = Lamp.GetComponent<Renderer>().material;
    }

    public void SetState(state _state)
    {
        lightState = _state;

        switch (lightState)
        {
            case state.OFF: // lamp turned off
                lampMaterial.color = Color.black;
                break;
            case state.DANGER: // lamp in danger zone
                lampMaterial.color = Color.red;
                break;
            case state.ON: // lamp NOT in danger zone
                lampMaterial.color = Color.green;
                break;
            case state.BLINK: // lamp goes to danger zone in any moment
                lampMaterial.color = Color.yellow;
                break;
            default:
                Debug.Log("unknown error occured");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            switch(lightState)
            {
                case state.DANGER:
                    Debug.LogError("EndGame");
                    //trigger here the timer stop.
                    Time.timeScale = 0;
                    break;
            }
        }
    }
}
