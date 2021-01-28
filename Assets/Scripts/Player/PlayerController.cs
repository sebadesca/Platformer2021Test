using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public float startSpeed = 10.0f;
    [SerializeField] float jumpForce = 10f;
    Rigidbody2D rb;
    [SerializeField] bool isOnGround = true;
    [SerializeField] bool airJump = true;
    bool facingRight = true;
    Transform playerGraphics;
    //[SerializeField] int startHealth = 100;
    //[SerializeField] int currentHealth;
    //[SerializeField] int maxHealth = 100;
    public float horizontalInput;


    GameManager gm;

    void Start()
    {
        //currentHealth = startHealth;
        //fireRate = 0.2f;
        //nextFire = Time.time;
        rb = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerGraphics = transform.Find("Body");
        if (playerGraphics == null)
        {
            Debug.Log("No player graphics!");
        }
    }

    void Update()
    {
        //if (gmScript.gameNotOver)
        //{
        //  PlayerAttackInput();
        //    gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) & isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) & airJump & !isOnGround)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            airJump = false;
        }
        if (horizontalInput>0 && !facingRight)
        {
            Flip();
        } else if (horizontalInput<0&&facingRight)
        {
            Flip();
        }

    }


    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        PlayerMovementInput();
    }

    void PlayerMovementInput()
    {
        //if (gmScript.gameNotOver)
        //{
            //float verticalInput = Input.GetAxis("Vertical");
            //transform.Translate(Vector3.up * Time.deltaTime * startSpeed * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * startSpeed * horizontalInput);
            //playerRb.MovePosition(positionRb);
     //   }
    }
    //void PlayerAttackInput()
    //{
    //    if (Input.GetButton("Fire1") && Time.time > nextFire)
    //    {
    //       Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    //      nextFire = Time.time + fireRate;
    // }

    //  if (Input.GetKey(KeyCode.Space) && slowEnergyScript.energy.energyAmount > 1)
    //  {
    //  }
    //}


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //  if (collision.CompareTag("Enemy") || collision.CompareTag("Enemy Proj"))
    //{
    //  ModifyHealth(-damageReceivedByEnemy);
    //}
    //}
    //public void ModifyHealth(int hpmod)
    //{
    //   currentHealth += hpmod;
    //  if (currentHealth > maxHealth)
    // {
    //    currentHealth = maxHealth;
    // }
    // else if (currentHealth < 1)
    //{
    //   currentHealth = 0;
    //gmScript.GameOver();
    //}
    //gmScript.ModifyHealthText(currentHealth);
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            airJump = true;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = playerGraphics.localScale;
        theScale.x *= -1;
        playerGraphics.localScale = theScale;
    }
}

