using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    float health;
    [SerializeField] float startHealth = 10f;
    [SerializeField] Image healthBar = null;
    [SerializeField] GameObject enemyCanvas = null;

    [Header("Enemy Pathing")]
    // Waypoints
    [SerializeField] float PatrolPointA = 0f;
    [SerializeField] float PatrolPointB = 0f;
    Vector3 pointA;
    Vector3 pointB;
    Vector2 waypointDirection;
    [SerializeField] bool patrolling = false;
    [SerializeField] bool isOnGround = true;



    // Start is called before the first frame update
    void Start()
    {
        pointA = new Vector3(PatrolPointA, transform.position.y, transform.position.z);
        pointB = new Vector3(PatrolPointB, transform.position.y, transform.position.z);
        health = startHealth;
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Pathing();

    }

    private void FixedUpdate()
    {
        if (isOnGround==true)
        { 
        Patrol(waypointDirection);
        }
    }

    void Pathing()
    {
        Vector2 directionToA = pointA - transform.position;
        Vector2 directionToB = pointB - transform.position;
        Vector2 direction;

        if (Vector2.Distance(pointA, transform.position) < 0.1f)
        {
            patrolling = true;
            direction = directionToB;
            direction.Normalize();
            waypointDirection = direction;
        }
        else if (Vector2.Distance(pointB, transform.position) < 0.1f && patrolling == true)
        {
            direction = directionToA;
            direction.Normalize();
            waypointDirection = direction;
        }
        else if (!patrolling)
        {
            direction = directionToA;
            direction.Normalize();
            waypointDirection = direction;
        }

    }

    void Patrol(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (waypointDirection * moveSpeed * Time.deltaTime));
    }


    public void Damage(float damage)
    {
        health -= damage;
        enemyCanvas.SetActive(true);
        healthBar.fillAmount = health / startHealth;



        if (health <= 0)
        {
            Death();
            return;
        }
    }

    private void Death()
    {
        moveSpeed = 0;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }

}
