using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject coin;
    public int numOfBullet, numOfCoin;
    public float timeShoot, angleGap;

    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", timeShoot, timeShoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 1; i <= numOfCoin; i++) Instantiate(coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        for (int i=1; i<=numOfBullet; i++)
        {
            GameObject bulletObj;
            bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObj.transform.up = target.transform.position - transform.position;
            if (i%2==0)
            {                
                bulletObj.transform.rotation = Quaternion.Euler(0, 0, bulletObj.transform.rotation.eulerAngles.z + (i / 2 * angleGap));
            }
            else
            {
                //Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -(i/2 * 20) + 180));
                bulletObj.transform.rotation = Quaternion.Euler(0, 0, bulletObj.transform.rotation.eulerAngles.z - (i / 2 * angleGap));
            }
        }
    }
}
