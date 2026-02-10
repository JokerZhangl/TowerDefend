using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;



public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject DiedBoom;
    NavMeshAgent agent;
     int Health;
     float Speed;
     int Reward;
   
    public float speed=> Speed;
    Transform targetTransform;
    // Start is called before the first frame update
    public void InitEnemy(int health,float seepd,int reward,Transform target)
    {
        Health=health;
        Speed=seepd;
        Reward=reward;                
        targetTransform = target;

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetTransform.position);
        agent.speed=Speed;
    }
    public void TakeDamage(int damage)
    {
       
        Health -=damage;
        if (Health<=0) {
         StartCoroutine (Death());
         ResourceManager.Instance.AddBatteries(Reward);
        }
    }
    IEnumerator Death()
    {   
        GameObject Boom=Instantiate(DiedBoom,this.transform.position+new Vector3(0,1f,0),this.transform.rotation);  
        Destroy(this.gameObject);
        Destroy(Boom,2f);
        GameManager.Instance.ReduceEnemy(1);
        Debug.Log("Enemy is Died");
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LifeTower"))
        {
            GameManager.Instance.GetHurt();
            StartCoroutine(Death());
        }
    }
}
