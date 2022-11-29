using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueManager : MonoBehaviour
{
    public static QueManager Instance;


    [Header("OBJECTS")]

    public List<GameObject> activatedQues = new List<GameObject>();

    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject atm1;
    public GameObject atm2;


    private void Awake()
    {
        Instance = this;

        // satýn alýndýklarýnda listeye eklenecekler
        activatedQues.Add(ticket1);
        activatedQues.Add(ticket2);
        activatedQues.Add(atm2);
    }
    
        

}
