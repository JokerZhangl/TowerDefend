using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager:Singleton<SelectionManager>
{
    TowerManager Selecttower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Selecter(TowerManager Tower)
    {
        if (Selecttower != null && Selecttower != Tower)
        {
            DeCurrentselecter();
        }

        Selecttower = Tower;
        Selecttower.SelectTower();
    }public void DeCurrentselecter()
    {
        if (Selecttower != null) 
        { Selecttower.DeSelectTower();
            UIManager.Instance.HideUI();
        Selecttower = null;
        }
       
    }
}
