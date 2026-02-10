using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    [SerializeField] TowerData[] availableTower;
    public bool IsSelectTower=false;
    TowerData CurrentTower;
    GameObject CurrentPrefab;
    int TowerLevel=0;
    float AttackRang = 0f;
    public int towerlevel=>TowerLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            IsSelectTower = true;
            SelectionManager.Instance.Selecter(this);
        }
      
      
    }
    public  void SelectTower()
    {
        if (TowerLevel > 0)
        {
            UIManager.Instance.ShowUpGradeUI(CurrentTower,this);
        }
        else
        {
            UIManager.Instance.ShowBuyUI(availableTower, this);
        }
          
    }
    public  void DeSelectTower()
    {
        IsSelectTower = false;
    } 
    public void BuyTower(TowerData tower)
    {
        if (tower != null) {
            CurrentTower=tower;
            if (ResourceManager.Instance.Batteries >= CurrentTower.buyCost) {
                ResourceManager.Instance.SpendBatteries(CurrentTower.buyCost);
               CurrentPrefab = Instantiate(CurrentTower.level1Prefab,this.transform.position,Quaternion.identity);
                
                CurrentPrefab.transform.SetParent(this.transform); 
                TowerLevel = 1;
                RefreshAttackRange(CurrentTower); 
                Debug.Log("购买完成");
                SelectionManager.Instance.DeCurrentselecter();

            }
            else
            {
                Debug.Log("电池不足");

            }
        }

        
    }
    public void UpGradeTower(TowerData tower)
    {
       
        if (TowerLevel == 1 && ResourceManager.Instance.Batteries >= CurrentTower.upgradeCost1)
        {
            Destroy(CurrentPrefab.gameObject);
            CurrentPrefab=Instantiate(CurrentTower.level2Prefab,this.transform.position,Quaternion.identity);
            CurrentPrefab.transform.SetParent (this.transform);
           
            TowerLevel = 2;
            RefreshAttackRange(CurrentTower);
            ResourceManager.Instance.SpendBatteries(CurrentTower.upgradeCost1);
            UIManager.Instance.ShowUpGradeUI(CurrentTower, this);
            Debug.Log("完成升级");
        }
        else if(TowerLevel == 2 && ResourceManager.Instance.Batteries >= CurrentTower.upgradeCost2)
        {
            Destroy(CurrentPrefab.gameObject);
            CurrentPrefab = Instantiate(CurrentTower.level3Prefab, this.transform.position, Quaternion.identity);
            CurrentPrefab.transform.SetParent(this.transform);
          
            TowerLevel = 3;
            RefreshAttackRange(CurrentTower);
            ResourceManager.Instance.SpendBatteries(CurrentTower.upgradeCost2);
            SelectionManager.Instance.DeCurrentselecter();
            Debug.Log("完成升级");
        }
        else
        {
            Debug.Log("电池不足");
        }
           
    }
    public void SellTower(TowerData tower)
    {
        if (tower != null)
        {
            Destroy(CurrentPrefab.gameObject);
            ResourceManager.Instance.AddBatteries(RefreshSellPrice());
            TowerLevel = 0;
            CurrentTower = null;
            Debug.Log("完成售卖");
            SelectionManager.Instance.DeCurrentselecter();
        }
       
    }
    public string RefreshUpPrice()
    {
        string price ="";
        switch(towerlevel)
        {
            case 1: price = CurrentTower.upgradeCost1.ToString(); break;
            case 2: price = CurrentTower.upgradeCost2.ToString(); break;
            case 3: price = "满级塔"; break;
              
        }
        return price;
    }
    public int RefreshSellPrice() { 
    int StartSellPrice= CurrentTower.buyCost;
    int SellPrice = StartSellPrice;
        switch (towerlevel) { 
        case 2:
                SellPrice += CurrentTower.upgradeCost1;
                break;
        case 3:
                SellPrice += (CurrentTower.upgradeCost1 + CurrentTower.upgradeCost2);
                break;
        }

        return Mathf.RoundToInt(SellPrice*0.8f);

    }
    void RefreshAttackRange(TowerData Tower)
    {
        switch (towerlevel)
        {
            case 1:
                AttackRang = Tower.level1Range;
                break;
            case 2:
                AttackRang = Tower.level2Range;
                break;
            case 3:
                AttackRang = Tower.level3Range;
                break;

        }
        CurrentPrefab.GetComponent<CapsuleCollider>().radius = AttackRang;
    }
}
