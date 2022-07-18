using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprRen;
    protected Animator anim;
    protected Collider2D coli;

    public GameObject player;
    public ParticleSystem vfx_Die;
    protected bool isDie = false;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        coli = gameObject.GetComponent<Collider2D>();

        //enabled = false;
    }

    private void LateUpdate()
    {
        if (HP <= 0 && isDie == false) Die();

    }
    public void GetHit(float dam)
    {
        //print(gameObject + " is hitted");
        HP -= dam;
        if (HP>0) anim.Play("GetHit");
    }
    public void Die()
    {
        isDie = true;

        coli.isTrigger = true;
        rb.gravityScale = 3;
        rb.velocity = new Vector2(5, 5);
        anim.applyRootMotion = false;
        anim.Play("Die");

        Instantiate(vfx_Die, transform.position, Quaternion.identity);

        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack") GetHit(1);
        if (collision.gameObject.tag == "SmallAttack") GetHit(0.5f);
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
    void OnBecameInvisible()
    {
        enabled = false;
    }
}
