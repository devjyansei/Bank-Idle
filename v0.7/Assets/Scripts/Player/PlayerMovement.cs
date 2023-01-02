using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public DynamicJoystick joystick;
    public float moveSpeed;
    public bool isMove;
    Animator anim;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            joystick.transform.GetChild(0).gameObject.SetActive(true);
            isMove = true;
        }
    }
    private void FixedUpdate()
    {
        if (isMove == true)
        {
            PlayerMove();
        }
        else
        {
            rb.velocity = Vector3.zero;
            joystick.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    
    private void PlayerMove()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, 0f, joystick.Vertical * moveSpeed);
        float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;

        if (angle != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }

        if (rb.velocity != Vector3.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}
