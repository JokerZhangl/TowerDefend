using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SlowTower : MonoBehaviour
{
    [SerializeField] List<Transform> AttackRangeEnemies;
    public TowerData tower;
    public Transform TargetTrans;


    void Start()
    {
       
    }

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            AttackRangeEnemies.Add(other.transform);
            if (AttackRangeEnemies.Contains(other.transform))
            {
                StartCoroutine(StartSlow(other.transform));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            AttackRangeEnemies.Remove(other.transform);
            if (!AttackRangeEnemies.Contains(other.transform)){
                StartCoroutine(ReturnSlow(other.transform));
            }

        }
    }
   
    
   
    
    IEnumerator StartSlow(Transform emnemys)
    {
        Enemy enemy= emnemys.GetComponent<Enemy>();
        float OrtginalSpeed= enemy.speed;
        float SlowSpeed = OrtginalSpeed * tower.SlowAmount;
        emnemys.GetComponent<NavMeshAgent>().speed=SlowSpeed;
        while (AttackRangeEnemies.Contains(emnemys))
        {
            yield return null;
        }
      
    }

    IEnumerator ReturnSlow(Transform emnemys)
    {

        yield return new WaitForSeconds(0.02f);
        if (!AttackRangeEnemies.Contains(emnemys))
        {
            float NowSpeed = emnemys.GetComponent<NavMeshAgent>().speed;
            float ReturnSpeed = NowSpeed / tower.SlowAmount;
            emnemys.GetComponent<NavMeshAgent>().speed = ReturnSpeed;
        }
      
       

    }
}
