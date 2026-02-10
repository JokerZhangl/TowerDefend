using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Transform TargetEnemy;
    float Speed;
    int Damage;


    private void Start()
    {
        Destroy(gameObject,2f);
    }
    public void SetTarget(Transform target,float speed,int damage)
    {
        TargetEnemy= target;
        if (target == null)
        {
            Destroy(gameObject);
        }
        Speed= speed;
        Damage= damage;
        StartCoroutine(BulletMove());
    }
    IEnumerator BulletMove()
    {
     /*   while (TargetEnemy != null) {
            Vector3 upOffest = new Vector3(0, 1f, 0);
            Vector3 targetpos=transform.position-TargetEnemy.position-upOffest;
            float distance = Speed * Time.deltaTime;
            transform.LookAt(TargetEnemy.position+ upOffest);
            transform.Translate(-targetpos.normalized *distance,Space.Self);
           yield return null;
        }*/

        while (TargetEnemy != null)
        {
            Vector3 upOffest = new Vector3(0, 1f, 0);
            Vector3 targetPosition = TargetEnemy.position;
            Vector3 direction = targetPosition - transform.position+upOffest;
            float distance = Speed * Time.deltaTime;

            // 1. 让导弹转向目标
            if (direction != Vector3.zero)
            {
                // 计算旋转，让导弹的forward指向目标
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    30f * Time.deltaTime
                );
            }

            // 2. 沿着导弹自身的forward方向前进（弹头方向）
            transform.Translate(Vector3.forward * distance, Space.Self);

            yield return null;
        }
        Destroy(this.gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
          
          Enemy enemy=other.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
              

                enemy.TakeDamage(Damage);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("1111");
            }
           
        }
    }

}
