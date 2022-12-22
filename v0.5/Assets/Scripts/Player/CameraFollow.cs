using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;    // kamera uzakligi
    [SerializeField] float chaseSpeed; // kamera takip hizi

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, chaseSpeed * Time.deltaTime);
    }
}
