using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform fireSource;
    [SerializeField] bool gun = true;

    // Prefab shooting
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate;
    [SerializeField] float nextFire;
    
    // Raycast shooting    
    [SerializeField] int laserDamage = 4;
    [SerializeField] LayerMask toHit;
    [SerializeField] float rayCastDist = 0f;
    [SerializeField] LineRenderer laserLine;
    // [SerializeField] GameObject projectilePrefab = null;
    // [SerializeField] GameObject blastPrefab = null;
    void Start()
    {
        if(fireSource == null)
        {
            Debug.Log("No Fire Source");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (gun==true)
            {
                fireRate = 0;
                gun = false;
            }
            else if (gun==false)
            {
                fireRate = 0.5f;
                gun = true;
            }
        }

        if (gun == false)
        {
            if (Input.GetButton("Fire1"))
            {
                StartCoroutine(ShootLaser());
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                ShootGun();
            }
        }
    }

    void ShootGun()
    {
        
        Instantiate(bullet, fireSource.transform.position, bullet.transform.rotation);

    }
    
    IEnumerator ShootLaser()
    {
        
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPos);
        Vector2 sourcePos = new Vector2(fireSource.position.x, fireSource.position.y);
        RaycastHit2D hitInfo = Physics2D.Raycast(sourcePos, mousePos - sourcePos, rayCastDist, toHit);
        Debug.DrawLine(sourcePos, (mousePos - sourcePos), Color.green);
        
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(sourcePos, hitInfo.point, Color.red);
            Debug.Log("We hit " + hitInfo.collider.name + " for " + laserDamage + "damage.");
            EnemyController enemy = hitInfo.transform.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Damage(laserDamage);
            }

            laserLine.SetPosition(0, fireSource.position);
            laserLine.SetPosition(1, hitInfo.point);

        } else
        {
            laserLine.SetPosition(0, fireSource.position);
            laserLine.SetPosition(1, fireSource.position + fireSource.right);
        }

        laserLine.enabled = true;
        yield return new WaitForSeconds(0.02f);
        laserLine.enabled = false;



    }
}
