using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 0;
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Rigidbody rig;
    [SerializeField]
    BoxCollider boxCollider;
    [SerializeField]
    bool inControll;

    private float speed=0;
    // Update is called once per frame
    void Update()
    {
        if (inControll)
        {
            Movement();
            JumpMovement();
        }
    }

    private void FixedUpdate()
    {
        
    }

    void JumpMovement()
    {

        SetJumpKey(KeyCode.Space, 10);
    }
    void Movement()
    {
        speed = Time.deltaTime * playerSpeed;
        SetInput(KeyCode.LeftArrow, Vector3.left, -1);
        SetInput(KeyCode.RightArrow, Vector3.right, 1);
        SetInput(KeyCode.UpArrow, Vector3.forward, 1);
        SetInput(KeyCode.DownArrow, Vector3.back, -1);
        
        
    }

    bool Isgrounded()
    {
        float extraHeight = .2f;

        if (Physics.Raycast(boxCollider.bounds.center, Vector3.down, boxCollider.bounds.extents.y + extraHeight)==false)
        {
            Debug.DrawRay(boxCollider.bounds.center, Vector3.down * (boxCollider.bounds.extents.y + extraHeight),Color.red);
            return false;
        }
        else
        {
            Debug.DrawRay(boxCollider.bounds.center, Vector3.down * (boxCollider.bounds.extents.y + extraHeight), Color.green);
        }
        return true;
    }

    void SetInput(KeyCode key, Vector3 SetTranslate,float inputSide)
    {
        if (Input.GetKey(key))
        {
            rig.MovePosition((playerTransform.position+(SetTranslate*speed)));
        }
    }

    void SetJumpKey(KeyCode key,float JumpSpeed)
    {
        if(Isgrounded() && Input.GetKey(key))
        {
            rig.velocity=Vector2.up * JumpSpeed;
        }
    }

 
}
