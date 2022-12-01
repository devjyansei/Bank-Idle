using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    public float goldAmount;
    private void Awake()
    {
        Instance = this;
        goldAmount = Mathf.Clamp(goldAmount, 0, 99999);
    }
    public void IncreaseGold(float amount)
    {
        goldAmount +=  amount;

    }
    public void DecraseGold(float amount)
    {
        goldAmount -= amount;
    }
}
