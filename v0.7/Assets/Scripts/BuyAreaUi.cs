using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuyAreaUi : MonoBehaviour
{
    string areaCost;

    public TextMeshProUGUI costText;
    private void Awake()
    {
        //costText.text = GetComponentInChildren<TextMeshPro>().text;
        areaCost = GetComponent<BuyArea>().cost.ToString();
        costText.text = areaCost;
    }

}
