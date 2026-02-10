using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] WaveData[] waveDatas;
    [SerializeField] Transform  SpawnPoint;
    [SerializeField] Transform  TargetPoint;

    float WaveTime = 25f;
    float EnemyTime = 3f;
    int CurrentWava = 0;
    int AllWava;
    // Start is called before the first frame update
    void Start()
    {
        AllWava= waveDatas.Length;
        GameManager.Instance.RefreshWave(CurrentWava, AllWava);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator WaveSpawn()
    {
        
        while(CurrentWava < waveDatas.Length){

            StartCoroutine(EnemySpawn(waveDatas[CurrentWava]));
            Debug.Log("完成第" + CurrentWava + "波");
            CurrentWava++;
            GameManager.Instance.RefreshWave(CurrentWava,AllWava);
            yield return new WaitForSeconds(WaveTime);     
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.OverAllRound();
       
    }
    IEnumerator EnemySpawn(WaveData wave)
    {
        for (int i = 0; i < wave.enemies.Length; i++) {
            for (int j = 0; j < wave.enemiesCount[i]; j++) {

                InitEnemy(wave.enemies[i]);
                
                Debug.Log("完成第" + i + "种敌人");
                yield return new WaitForSeconds(EnemyTime);
            }


        }
    }
    public void InitEnemy(EnemiesData data)
    {
        
   GameObject enemy=Instantiate(data.EnemiesPrefab, SpawnPoint.position, Quaternion.identity);
        Enemy enemycs=enemy.GetComponent<Enemy>();
        GameManager.Instance.AddEnemy(1);
        enemycs.InitEnemy(data.Health, data.Speed, data.Reward, TargetPoint);
    }
   public void StartRound()
    {
        StartCoroutine(WaveSpawn());
    }
    public void StopSpanw()
    {
        StopAllCoroutines();
    }
}
