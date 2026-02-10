using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] int batteries = 1000;
   public int Batteries => batteries; // Start is called before the first frame update
    void Start()
    {
        RefreshBattery();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpendBatteries(int Cost)
    {
        batteries -= Cost;
        RefreshBattery();
    }
    public void AddBatteries(int Cost)
    {
        batteries += Cost;
        RefreshBattery();
    }
    void RefreshBattery()
    {
        UIManager.Instance.InitUI(batteries);
    }
    
}
