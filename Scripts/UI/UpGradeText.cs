using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpGradeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pricetext;
    // Start is called before the first frame update
  
    public void Setpricetext(string price)
    {
        pricetext.text=price;
    }
}
