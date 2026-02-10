using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="New TowerData",menuName ="Tower Defence/Tower Data")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public Sprite towerIcon;

    [Header("Prefabs")]
    public GameObject level1Prefab;
    public GameObject level2Prefab;
    public GameObject level3Prefab;

    [Header("Cost")]
    public int buyCost;
    public int upgradeCost1;
    public int upgradeCost2;

    [Header("Stats")]
    public float level1Range;
    public float level2Range;
    public float level3Range;

    [Header("Add Stats")]
    public int Damage;
    public float fireRate;
    public float bulletRate;
    public  float RoteSpeed;
    [Range(0, 1)]
    public float SlowAmount;


}
