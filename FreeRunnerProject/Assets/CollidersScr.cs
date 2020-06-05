using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersScr : MonoBehaviour
{
    [SerializeField]
    Rigidbody rig;
    float jumpSpeed = 4;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Jump")
        {
            rig.velocity = Vector2.up * jumpSpeed;
        }
        if (other.gameObject.tag == "JumpHigher")
        {
            rig.velocity = Vector2.up * 7;
        }
    }
}
