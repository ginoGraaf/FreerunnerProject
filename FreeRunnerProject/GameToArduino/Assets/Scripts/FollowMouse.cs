using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
