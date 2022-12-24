using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public IEnumerator followCoroutine;

    public void SelfDestroy()
    {
      
        Destroy(gameObject);
    }

    public void Follower()
    {
        MoneyBag moneyBag = transform.GetComponentInParent<MoneyBag>();

        if (moneyBag.moneyList.Count > 0)
        {
            for (int i = 0; i < moneyBag.moneyList.Count; i++)
            {
                transform.localPosition = new Vector3(0, moneyBag.moneyList.Count * 0.1f, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        

    }
}
