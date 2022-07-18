using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMon : Enemy
{
    public LayerMask groundLayer;

    public int direct;
    public float speed, speedTime, changeTime;
    public bool[] smallCheckDirectArr = new bool[4];

    float size;
    public bool isCanMove;
    bool isAlreadyInvoke;
    public ParticleSystem vfx_DustEffect;
    public GameObject afterImage;

    AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        size = transform.localScale.z / 2;
        isCanMove = true;
        enabled = false;
    }


    void FixedUpdate()
    {
        if (isDie == false)
        {
            SetDirectArr();
            if (smallCheckDirectArr[direct] == true)
            {
                //Thoi diem bat dau tiep xuc voi ground
                MakeDustEffect();
                CancelInvoke();
                ChangeDirect();
                isCanMove = false;
                isAlreadyInvoke = false;
                CancelInvoke();
            }
            if (isCanMove == false && isAlreadyInvoke == false && enabled == true)
            {
                isAlreadyInvoke = true;
                Invoke("CanMove", changeTime);
            }
            if (isCanMove) Move();
            else rb.velocity = Vector2.zero;
        }
    }
    void SetDirectArr()
    {
        smallCheckDirectArr[0] = Physics2D.OverlapCircle(transform.position + new Vector3(0f, size, 0) / 1.9f, size / 2, groundLayer);
        smallCheckDirectArr[1] = Physics2D.OverlapCircle(transform.position + new Vector3(size, 0, 0) / 1.9f, size / 2, groundLayer);
        smallCheckDirectArr[2] = Physics2D.OverlapCircle(transform.position + new Vector3(0f, -size, 0) / 1.9f, size / 2, groundLayer);
        smallCheckDirectArr[3] = Physics2D.OverlapCircle(transform.position + new Vector3(-size, 0, 0) / 1.9f, size / 2, groundLayer);

        
    }

    void Move()
    {
        if (direct == 0) rb.velocity = Vector2.Lerp(rb.velocity, Vector2.up * speed,speedTime*Time.fixedDeltaTime);
        if (direct == 1) rb.velocity = Vector2.Lerp(rb.velocity, Vector2.right * speed, speedTime*Time.fixedDeltaTime);
        if (direct == 2) rb.velocity = Vector2.Lerp(rb.velocity, Vector2.down * speed, speedTime*Time.fixedDeltaTime);
        if (direct == 3) rb.velocity = Vector2.Lerp(rb.velocity, Vector2.left * speed, speedTime*Time.fixedDeltaTime);
    }

    void DoAfterImage()
    {
        GameObject obj;
        obj = Instantiate(afterImage, transform.position, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = sprRen.sprite;
    }

    void ChangeDirect()
    {
        direct += 2;
        direct %= 4;
    }

    void MakeDustEffect()
    {
        if (enabled == true)
        {
            music.Play();
            CameraController.LightShake();
        }

        if (direct == 0) Instantiate(vfx_DustEffect, transform.position + new Vector3(0, size / 1f), Quaternion.identity);
        if (direct == 1) Instantiate(vfx_DustEffect, transform.position + new Vector3(size / 1f, 0), Quaternion.Euler(0,0,90));
        if (direct == 2) Instantiate(vfx_DustEffect, transform.position + new Vector3(0, -size / 1f), Quaternion.identity);
        if (direct == 3) Instantiate(vfx_DustEffect, transform.position + new Vector3(-size / 1f, 0), Quaternion.Euler(0,0,90));
    }

    void CanMove()
    {
        //print("T");
        isCanMove = true;
        InvokeRepeating("DoAfterImage",0.1f, 0.1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //nothing
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(size, 0, 0) / 1.9f, size / 2);
    }

    void OnBecameVisible()
    {
        //print("Vi " + IsInvoking("CanMove"));
        if (IsInvoking("CanMove") == false) Invoke("CanMove", changeTime);
        enabled = true;
    }
    void OnBecameInvisible()
    {
        //print("Invi " + IsInvoking("CanMove"));
        CancelInvoke();
        enabled = false;
    }
}
