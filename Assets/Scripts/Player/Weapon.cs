using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate;
    [SerializeField] float nextFire;
    [SerializeField] int damage = 10;
    [SerializeField] LayerMask toHit;
    [SerializeField] Transform fireSource;
    [SerializeField] float rayCastDist = 0f;
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
        if (fireRate == 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Debug.Log("fireRate0");
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Test");
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 sourcePos = new Vector2(fireSource.position.x, fireSource.position.y);
        RaycastHit2D hit = Physics2D.Raycast(sourcePos, mousePos - sourcePos, rayCastDist, toHit);
        Debug.DrawLine(sourcePos, (mousePos-sourcePos)*rayCastDist, Color.green);
        if (hit.collider !=null)
        {
            Debug.DrawLine(sourcePos, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " for " + damage + "damage.");
        }
        Instantiate(bullet, transform.position, bullet.transform.rotation);

    }

}
