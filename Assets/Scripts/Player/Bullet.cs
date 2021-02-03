using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 target;
    Vector3 direction;
    float range = 1f;
    [SerializeField] float speed = 0f;
    [SerializeField] float damage = 1;
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] ParticleSystem particleSystem;
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        direction = (target - transform.position).normalized;
        rb.velocity = direction * speed;
        Destroy(gameObject, range);
    }

    void Update()
    {
        //float step = speed * Time.deltaTime;
        //transform.Translate(direction * step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.name + " has collided with " + collision.name);
        if (!collision.gameObject.CompareTag("Player"))
        {


            if (collision.gameObject.CompareTag("Enemy"))
            {
                //calling EnemyController.Damage(damage) 
                collision.gameObject.GetComponent<EnemyController>().Damage(damage);
                //GameObject.Instantiate(particleSystem, transform.position, transform.rotation);
            }
            Destroy(gameObject);

        }
    }
}
