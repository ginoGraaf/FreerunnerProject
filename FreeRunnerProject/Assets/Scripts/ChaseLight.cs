using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLight : MonoBehaviour
{
    [SerializeField] private GameObject Lamp;
    private Material lampMaterial;

    void Start()
    {
        lampMaterial = Lamp.GetComponent<Renderer>().material;

        SetState(Color.yellow);
    }

    public void SetState(Color color)
    {
        lampMaterial.color = color;
    }
}
