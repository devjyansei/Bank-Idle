using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public FloatingJoystick floatingJoyStick;
    Rigidbody rb;

    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        rb.velocity = new Vector3(floatingJoyStick.Horizontal * moveSpeed, 0f, floatingJoyStick.Vertical * moveSpeed);
    }
}
