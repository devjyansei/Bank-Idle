using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public int goldAmount;
    private void Awake()
    {
        Instance = this;
    }
    public void IncreaseGold(int amount)
    {
        goldAmount +=  amount;

    }
    public void DecraseGold(int amount)
    {
        goldAmount -= amount;
    }
}
