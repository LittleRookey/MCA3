using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;
    public Transform target;
    public float moveSpeed;
    public GameObject particle;
    public GameObject item;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        float step = moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, target.position);

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }
    
    void Die()
    {
        // drop item by 50 % 
        float perc = Random.Range(0, 100f); 
        if (perc < 20f) // Drop Item
        {
            Instantiate(item, transform.position+ Vector3.up, Quaternion.identity);
        }
        var par = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(par.gameObject, 1f);
        EnemySpawner.Instance.enemies.Remove(this.gameObject);
        EnemySpawner.Instance.killedEnemyNum += 1;
        EnemySpawner.Instance.enemyAllDead();
        EnemySpawner.Instance.UpdateUI();
        Destroy(gameObject);
        

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().LoseHealth(damage);
            
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //enemy die
            Die();
            
        }
    }
}
