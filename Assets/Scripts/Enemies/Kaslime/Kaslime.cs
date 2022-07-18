using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaslime : Enemy
{
    public Transform whitePlayer, blackPlayer;
    public float speedGroundJump, moveSpeed, timeDoTopGroundJump;
    public bool isTopGroundJump, isGroundJump, isMoveToTarget, isFollowWhite;
    bool isDoGroundJump;

    public GameObject kaslimeTransformObj, BossHPSlider;
    public GameObject afterImage;

    public ParticleSystem vfx_DustEffect;

    public AudioClip landingClip, fallingClip;
    AudioSource music;
    public AudioSource bgMusic;

    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        BossHPSlider.SetActive(true);
        isGroundJump = true;
        InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
        music.clip = fallingClip;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isGroundJump", isGroundJump);
        anim.SetBool("isTopGroundJump", isTopGroundJump);
        anim.SetBool("isMoveToTarget", isMoveToTarget);
        anim.SetFloat("xSpeed", rb.velocity.x);
    }

    private void FixedUpdate()
    {
        if (HP <= 0)
        {
            if (bgMusic.volume>0) bgMusic.volume -= 0.02f;
            kaslimeTransformObj.SetActive(true);
        }
        if (HP > 0)
        {
            if (isTopGroundJump)
            {
                if (isDoGroundJump == false)
                {
                    isDoGroundJump = true;
                    Invoke("StopDoTopGroundJump", timeDoTopGroundJump);
                }
                ToTopGroundJump(target + new Vector3(0, 10, 0));
            }
            if (isGroundJump) GroundJump();
            if (isMoveToTarget)
            {
                if (isFollowWhite) MoveToTarget(whitePlayer.position);
                else MoveToTarget(blackPlayer.position);
            }
        }
    }

    void ToTopGroundJump(Vector3 target)
    {
        rb.gravityScale = 0;
        transform.position = Vector3.Lerp(transform.position, target, speedGroundJump*Time.fixedDeltaTime);
        //print(Mathf.Abs(transform.position.y - target.y) + " " + target);
        //if (Mathf.Abs(transform.position.y - target.y) < 2f)
        //{
        //    isTopGroundJump = false;
        //    isGroundJump = true;
        //    InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
        //    music.clip = fallingClip;
        //    music.Play();
        //    rb.velocity = new Vector2(0, 0);
        //}
    }

    void StopDoTopGroundJump()
    {
        isTopGroundJump = false;
        isGroundJump = true;
        InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
        music.clip = fallingClip;
        music.Play();
        rb.velocity = new Vector2(0, 0);
        isDoGroundJump = false;
    }

    void GroundJump()
    {
        rb.gravityScale = 3;
        //rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void MoveToTarget(Vector3 target)
    {
        if (target.x > transform.position.x)
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
        else
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);

        if (Mathf.Abs(transform.position.x - whitePlayer.position.x) < 5 || Mathf.Abs(transform.position.x - blackPlayer.position.x) < 5)
        {
            isTopGroundJump = true;
            isMoveToTarget = false;
        }
    }

    void SetTrueMoveToTarget()
    {
        //print("Y");
        if (isMoveToTarget == false)
        {
            //print("Y");
            isMoveToTarget = true;
            if (Random.Range(0, 10) < 5)
            {
                isFollowWhite = true;
                target = whitePlayer.transform.position;
            }
            else
            {
                isFollowWhite = false;
                target = blackPlayer.transform.position;
            }
        }
    }

    void DoAfterImage()
    {
        GameObject obj;
        obj = Instantiate(afterImage, transform.position, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = sprRen.sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke();
            isDoGroundJump = false;
            music.clip = landingClip;
            music.Play();
            Instantiate(vfx_DustEffect, transform.position, Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(1,0,0), Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
            CameraController.Shake();
            isGroundJump = false;
            isMoveToTarget = false;
            //isMoveToTarget = true;
            Invoke("SetTrueMoveToTarget", 0.6f);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            CancelInvoke();
            isDoGroundJump = false;
            music.clip = landingClip;
            music.Play();
            Instantiate(vfx_DustEffect, transform.position, Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(-1,0,0), Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            Instantiate(vfx_DustEffect, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
            CameraController.Shake();
            isGroundJump = false;
            isMoveToTarget = false;
            //isMoveToTarget = true;
            Invoke("SetTrueMoveToTarget", 1);
        }
        if (collision.gameObject.tag == "Attack") GetHit(1);
        if (collision.gameObject.tag == "SmallAttack") GetHit(0.5f);
        
    }
}
