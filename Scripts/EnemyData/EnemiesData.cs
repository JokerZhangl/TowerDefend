using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Tower Defence/Enemies")]
public class EnemiesData : ScriptableObject
{
    [SerializeField]
    public string EnemiesName;
    public GameObject EnemiesPrefab;
    public float AttactDamage;
    public int Health;
    public float Speed;
    public int Reward;

}
