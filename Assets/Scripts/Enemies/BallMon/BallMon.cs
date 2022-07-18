using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMon : Enemy
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(speed, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0) sprRen.flipX = true;
        else sprRen.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.Play("Touch");
        if (collision.gameObject.tag == "Attack") GetHit(1);
        if (collision.gameObject.tag == "SmallAttack") GetHit(0.5f);
    }
}
