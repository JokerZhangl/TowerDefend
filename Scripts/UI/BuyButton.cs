using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BuyCost;
    [SerializeField] Image BuyUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitBuyButton(Sprite buyUI,int Cost)
    {
        BuyCost.text="$"+Cost.ToString();
      this.BuyUI.sprite= buyUI;

    }
}
