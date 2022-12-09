using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public IEnumerator followCoroutine;
    private void OnEnable()
    {
       // followCoroutine = FollowIndex(transform);
    }
    public void SelfDestroy()
    {
      
        Destroy(gameObject);
    }
    public IEnumerator FollowIndex(Transform target)
    {
        StopCoroutine(followCoroutine);
        while (true)
        {            
            transform.position = target.position;
            
            yield return null;
        }

    }
    public void Follower()
    {
        var moneyBag = transform.GetComponentInParent<MoneyBag>();

        if (moneyBag.moneyList.Count > 0)
        {
            for (int i = 0; i < moneyBag.moneyList.Count; i++)
            {
                
                transform.position = moneyBag.moneyPositions[i].position;

            }
        }
        

    }
}
