using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public static Variables Instance;

    public int priority;

    [Range(0,100)]
    public float moneyValue = 10f;

    private void Awake()
    {
        Instance = this;
    }
}
