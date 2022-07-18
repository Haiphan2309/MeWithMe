using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    public GameObject bombMon;
    public float timeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", timeSpawn, timeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(bombMon, transform.position, Quaternion.identity);
    }

    void OnBecameVisible()
    {
        //print("V");
        enabled = true;
        if (IsInvoking("Spawn") == false)
            InvokeRepeating("Spawn", timeSpawn, timeSpawn);
    }
    void OnBecameInvisible()
    {
        //print("I");
        enabled = false;
        CancelInvoke();
    }
}
