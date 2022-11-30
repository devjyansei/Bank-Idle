using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            Destroy(this.gameObject);
        }
       
    }
   
}
