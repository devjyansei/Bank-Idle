using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public Transform currentTarget;
    [SerializeField] Vector3 offset;    // kamera uzakligi
    public float chaseSpeed; // kamera takip hizi
    private void Awake()
    {
        currentTarget = playerTarget;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentTarget.position + offset, chaseSpeed * Time.deltaTime);
    }
    public void TemporaryCameraSwitch(Transform newTarget)
    {
        playerTarget.GetComponent<PlayerMovement>().enabled = false;
        chaseSpeed = 1;
        currentTarget = newTarget;
        Invoke("SetCameraTargetToDefault", 3f);
    }
    public void SetCameraTargetToDefault()
    {
        playerTarget.GetComponent<PlayerMovement>().enabled = true;
        currentTarget = playerTarget;
        Invoke("SetChaseSpeedToDefault", 2f);
    }
    public void SetChaseSpeedToDefault()
    {
        chaseSpeed = 10f;
    }
}
