using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public FloatingJoystick floatingJoyStick;
    Rigidbody rb;

    bool isDragStarted;
    Vector3 touchDown, touchUp;
    public float rotationSpeed;
    Touch touch;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)    
                                                    
            {
                isDragStarted = true;   
                touchDown = touch.position;
                touchUp = touch.position;
            }
        }

        if (isDragStarted)  
        {
            if (touch.phase == TouchPhase.Moved)    
            {
                touchDown = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)    
            {
                touchDown = touch.position;
                isDragStarted = false;
            }
           transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);

        }
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(floatingJoyStick.Horizontal * moveSpeed, 0f, floatingJoyStick.Vertical * moveSpeed);

    }
    private Quaternion CalculateRotation()
    {
        Vector3 direction = (touchDown - touchUp).normalized;

        direction.z = direction.y;    
        direction.y = 0;
      
        Quaternion rotate = Quaternion.LookRotation(direction, Vector3.up);   

        return rotate;
    }

}

