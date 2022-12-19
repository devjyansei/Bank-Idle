using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 500f;

    Touch touch; 
    Vector3 touchDown;
    Vector3 touchUp;

    bool isDragStarted;
    

   
    private void Update()
    {
        if (Input.touchCount > 0) // ekrana birisi dokunuyorsa
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)    // touchphase dokunma durumudur. / Began basladigi zaman / Ended bittigi zaman
                                                    // Canceled birakildigi zaman / moved hareket ettigi zaman
            {
                isDragStarted = true;   // parmagi ekrana surtme islemi basladi
                touchDown = touch.position;
                touchUp = touch.position;
            }
        }

        if (isDragStarted)  // user parmagini koymussa
        {
            if (touch.phase == TouchPhase.Moved)    // user parmagini hareket ettirirse
            {
                touchDown = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)    // user parmagini kaldirirsa
            {
                touchDown = touch.position;
                isDragStarted = false;
            }
            if (CalculateDirection() != Vector3.zero)
            {
                gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);// nereden nereye rotate edecegi

            }
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    Quaternion CalculateRotation()
    {
        Quaternion rotate = Quaternion.LookRotation(CalculateDirection(), Vector3.up);   // verilen vectore dogru bakar. hangi eksende bakacagi.
        return rotate;
    }

    Vector3 CalculateDirection() // user in parmagiyla cizdigi cizginin vector halini return eder.
    {
        Vector3 direction = (touchDown - touchUp).normalized;

        direction.z = direction.y;    // y ekseninde parmak hareketi, artik z ekseninide degistirecek.
        direction.y = 0;

        return direction;
    }
}
