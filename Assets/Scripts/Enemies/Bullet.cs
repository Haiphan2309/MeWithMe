using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Rigidbody2D rb;
    public ParticleSystem vfx_Destroy, vfx_Dust;
    ParticleSystem vfx;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        vfx = Instantiate(vfx_Dust, transform.position, Quaternion.identity);
        //rb = gameObject.GetComponent<Rigidbody2D>();
        //rb.velocity = transform.up.normalized * speed;
    }

    private void Update()
    {
        vfx.transform.position = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed*Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(vfx_Destroy, transform.position - new Vector3(0,0.5f,1), Quaternion.identity);
        Destroy(gameObject,0.1f);
    }
}
