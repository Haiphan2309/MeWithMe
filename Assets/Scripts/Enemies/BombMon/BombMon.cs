using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMon : Enemy
{
    public CheckGroundFront checkForwalkScr;

    public float speed, rollSpeed, timePrepareExplosion;
    public bool isMoveRight, isLightBombMon, isRolling;

    public GameObject explosionObj;
    // Start is called before the first frame update
    void Start()
    {
        if (isLightBombMon == false)
            anim.SetBool("isPrepareExplosion", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 9)
        {
            Explosion();
        }
        else
        {
            if (isLightBombMon == false && HP < 11 && anim.GetBool("isPrepareExplosion") == false)
            {
                anim.SetBool("isPrepareExplosion", true);
                Invoke("Explosion", timePrepareExplosion);
            }
            if (isLightBombMon && HP < 11 && anim.GetBool("isRolling") == false)
            {
                isRolling = true;
                anim.SetBool("isRolling", true);
                anim.SetBool("isRolling", isRolling);
                Invoke("Explosion", timePrepareExplosion);
            }
        }

 
            if (isLightBombMon)
            {
                if (isMoveRight) transform.localScale = new Vector3(-1, 1, 1);
                else transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (isMoveRight) transform.rotation = Quaternion.Euler(0, 180, 0);
                else transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        
    }

    private void FixedUpdate()
    {
        if (isDie == false)
        {
            if (isRolling) Roll();
            else Move();
        }
    }
    void Move()
    {
        if (checkForwalkScr.isGroundFront) isMoveRight = !isMoveRight;

        if (isMoveRight)
            rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Explosion()
    {
        Instantiate(explosionObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Roll()
    {
        if (isMoveRight) rb.velocity = new Vector2(rollSpeed * Time.fixedDeltaTime, rb.velocity.y);
        else rb.velocity = new Vector2(-rollSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRolling) Explosion();
        if (collision.tag == "EnemyAttack") GetHit(3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLightBombMon)
        {
            if (collision.gameObject.tag == "Attack")
            {
                if (collision.transform.position.x > transform.position.x) isMoveRight = false;
                else isMoveRight = true;
                anim.SetBool("isMoveRight", isMoveRight);
                GetHit(1);
            }
            if (collision.gameObject.tag == "SmallAttack")
            {
                if (collision.transform.position.x > transform.position.x) isMoveRight = false;
                else isMoveRight = true;
                anim.SetBool("isMoveRight", isMoveRight);
                GetHit(0.5f);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Attack") GetHit(1);
            if (collision.gameObject.tag == "SmallAttack") GetHit(0.5f);
        }
    }
}
