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
    private Animator lampAnimator;

    void Awake()
    {
        lampMaterial = Lamp.GetComponent<Renderer>().material;
        lampAnimator = Lamp.GetComponent<Animator>();
    }

    public void SetState(state _state)
    {
        lightState = _state;
        lampAnimator.SetBool("Blink", false);

        switch (lightState)
        {
            case state.OFF: // lamp turned off
                lampMaterial.color = Color.black;
                break;
            case state.DANGER: // lamp in danger zone
                lampAnimator.SetTrigger("Danger");
                break;
            case state.ON: // lamp NOT in danger zone
                lampAnimator.SetTrigger("On");
                break;
            case state.BLINK: // lamp goes to danger zone in any moment
                lampAnimator.SetTrigger("Blink");
                break;
            default:
                Debug.Log("unknown error occured");
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            switch(lightState)
            {
                case state.DANGER:
                    Debug.Log("EndGame");
                    //trigger here the timer stop.
                    Time.timeScale = 0;
                    break;
                   
            }
        }
    }
}
