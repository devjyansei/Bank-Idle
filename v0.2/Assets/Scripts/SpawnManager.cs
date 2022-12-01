using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform customerSpawnPosition;
    public GameObject customerPrefab;
    QueOrder queOrder;
    [Range(0,100)]
    public float spawnInterval;

    private void Awake()
    {
        queOrder = GameObject.Find("Security").GetComponent<QueOrder>();
    }
    private void Start()
    {
        StartSpawner();
    }
    void StartSpawner()
    {
        StartCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitUntil(() => queOrder.customerList[queOrder.customerList.Count - 1] == null);
            GameObject tempCustomer = Instantiate(customerPrefab);
            tempCustomer.transform.position = customerSpawnPosition.position;

            yield return new WaitForSeconds(spawnInterval);
        }

    }


}
