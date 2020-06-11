using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLight : MonoBehaviour
{
    [SerializeField] private GameObject Lamp;
    [HideInInspector] public int state;
    private Material lampMaterial;

    void Awake()
    {
        lampMaterial = Lamp.GetComponent<Renderer>().material;
    }

    public void SetState(int _state)
    {
        state = _state;

        switch (state)
        {
            case 0: // lamp turned off
                lampMaterial.color = Color.black;
                break;
            case 1: // lamp in danger zone
                lampMaterial.color = Color.red;
                break;
            case 2: // lamp NOT in danger zone
                lampMaterial.color = Color.green;
                break;
            default:
                Debug.Log("unknown error occured");
                break;
        }
    }
}
