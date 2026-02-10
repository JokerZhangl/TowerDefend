using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("¹ºÂòÓëÉý¼¶")]
    [SerializeField] GameObject buyUI;
    [SerializeField] GameObject buyButtonPrefab;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject upgradeButtonPrefab;
    [SerializeField] GameObject sellButtonPrefab;
    [SerializeField] TextMeshProUGUI Batteris;
    [SerializeField] float buttonRadius = 50;
     [SerializeField] float B = 50;

    TowerManager CurrentTower;
    // Start is called before the first frame update
    void Start()
    {
        buyUI.SetActive(false);
    }

  public  void InitUI(int ui)
    {
        Batteris.text = ui.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(upgradeUI.activeSelf == true || buyUI.activeSelf == true)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    HideUI();
                }
            }
            else
            {
                Debug.Log("12125");
            }
            
        }
    }
    public void ShowBuyUI(TowerData[] availableTower, TowerManager tower)
    {
        CurrentTower = tower;
        foreach (Transform child in buyUI.transform)
        {
            Destroy(child.gameObject);
        }
        int TowerCount = availableTower.Length;
        for (int i = 0; i < TowerCount; i++)
        {

            GameObject buyButton = Instantiate(buyButtonPrefab, buyUI.transform);
            Vector2 buttonPostion = GetButtonPos(i, TowerCount);
            buyButton.transform.localPosition = buttonPostion;
            Button button=buyButton.GetComponent<Button>();
            buyButton.GetComponent<BuyButton>().InitBuyButton(availableTower[i].towerIcon, availableTower[i].buyCost);
            print(i);
            int A = i;
            button.onClick.AddListener(()=>tower.BuyTower(availableTower[A]));
        }
        Vector3 towerPos = Camera.main.WorldToScreenPoint(CurrentTower.transform.position);
        print(towerPos);
        buyUI.transform.position = towerPos+new Vector3(0,B,0);
        buyUI.SetActive(true);
    }
    Vector2 GetButtonPos(int Index,int towercount)
    {
        float angleStep = 360 / towercount;
        float angle = Index * angleStep+90;
        float angleRad=angle*Mathf.Deg2Rad;

        float x=Mathf.Cos(angleRad)*buttonRadius;
        float y=Mathf.Sin(angleRad)*buttonRadius;
        return new Vector2(x, y);

    }
    public void HideUI() {

        buyUI.SetActive(false);
        upgradeUI.SetActive(false);
        CurrentTower = null;
    }
    public void ShowUpGradeUI(TowerData towerData,TowerManager tower) {

        CurrentTower = tower;
        foreach (Transform child in upgradeUI.transform)
        {
            Destroy(child.gameObject);
        }
        //Éý¼¶Ëþ
        GameObject upgradeButton = Instantiate(upgradeButtonPrefab, upgradeUI.transform);
        upgradeButton.transform.GetComponent<UpGradeText>().Setpricetext(CurrentTower.RefreshUpPrice());
        Vector2 upbuttonPostion = GetButtonPos(0, 2);
        upgradeButton.transform.localPosition = upbuttonPostion;
        Button upbutton = upgradeButton.GetComponent<Button>();
        upbutton.onClick.AddListener(() => tower.UpGradeTower(towerData));

        //ÂôËþ
        GameObject SellButton = Instantiate(sellButtonPrefab, upgradeUI.transform);
        SellButton.transform.GetComponent<UpGradeText>().Setpricetext(CurrentTower.RefreshSellPrice().ToString());
        Vector2 sellbuttonPostion = GetButtonPos(0, 2);
        upgradeButton.transform.localPosition = sellbuttonPostion;
        Button sellbutton = SellButton.GetComponent<Button>();
        sellbutton.onClick.AddListener(() => tower.SellTower(towerData));

        Vector3 towerPos = Camera.main.WorldToScreenPoint(CurrentTower.transform.position);
        upgradeUI.transform.position = towerPos + new Vector3(0, B, 0);
        upgradeUI.SetActive(true);
    }
}
