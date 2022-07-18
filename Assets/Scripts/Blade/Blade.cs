using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    Animator anim;
    public float timeExist, speed;

    public ParticleSystem vfx_Destroy;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", timeExist);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") Destroy(gameObject);
    }

    void End()
    {
        anim.Play("End");
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(vfx_Destroy, transform.position, Quaternion.identity);
    }
}
