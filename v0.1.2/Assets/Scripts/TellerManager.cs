using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellerManager : MonoBehaviour
{

    public List<GameObject> createdMoneyList = new List<GameObject>();

    public int stackLimit;
    public GameObject moneyPrefab;
    public Transform spawnPoint;

    public IEnumerator ticketCoroutine;

    public bool isQueFull;

    QueOrder queOrder;




}
