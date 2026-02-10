using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] List<Transform> AttackRangeEnemies;
    [SerializeField] List<Transform> BulletSpawnTrans;
    [SerializeField]GameObject BulletPrefab;
    public  TowerData tower; 
    public  Transform TargetTrans;


    void Start()
    {
        StartCoroutine(AttackEnmies());
    }
   
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            AttackRangeEnemies.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            AttackRangeEnemies.Remove(other.transform);
        }
    }
    IEnumerator AttackEnmies()

    {bool isAttacked = false;
        float a = 2f;
        while (true) {
            a-=Time.deltaTime;
            if(a <= 0f)
            {
                isAttacked = true;
            }
            if(AttackRangeEnemies.Count > 0)
            {
               
                TargetTrans = GetNearestEnemy();

                if (TargetTrans != null)
                {
                    RotateTargetEnm();
                    if (isAttacked) {
                      StartCoroutine(SpawnBullet());
                        isAttacked = false;
                        a = tower.fireRate;
                    }
                   
                   
                    
                }
                else
                {
                    Debug.Log("Ã»Ä¿±ê");
                }
               
            }
            yield return null;
        }
      
    }
    Transform GetNearestEnemy()
    {
       
        float BestDistance = Mathf.Infinity;
        Transform NearestEnemy = null;
        AttackRangeEnemies.RemoveAll(transform=>transform==null);
        foreach (Transform t in AttackRangeEnemies)
        {
            float distance=Vector3.Distance(t.position,this.transform.position);
            if (distance < BestDistance)
            {
                BestDistance = distance;
                NearestEnemy= t;
            }
        }
        return NearestEnemy;
    }
    void RotateTargetEnm()
    {
        
        Vector3 Pos = TargetTrans.position-this.transform.position;
          Pos.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(Pos);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion,Time.deltaTime* tower.RoteSpeed);
    }
    IEnumerator SpawnBullet() 
    { 
        foreach(Transform trans in BulletSpawnTrans)
        {
            GameObject bullet=Instantiate(BulletPrefab, trans.position,trans.rotation);
            Bullet bullet1=bullet.GetComponent<Bullet>();
            if(TargetTrans != null&& bullet1 != null)
            {
                bullet1.SetTarget(TargetTrans, tower.bulletRate,tower.Damage);
                
            }
            yield return new WaitForSeconds(0.5f);
        }
    
    }
}
