using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class toggleAutoconnect : MonoBehaviour
{
    public Toggle toggle;
    public Dropdown COMPort;

    private void Start()
    {
        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(delegate { ToggleValueChanged(); });
    }

    void ToggleValueChanged()
    {
        COMPort.interactable = !toggle.isOn;
    }
}
