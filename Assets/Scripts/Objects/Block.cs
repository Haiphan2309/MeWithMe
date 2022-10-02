using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    SpriteRenderer sprRen;
    Animator anim;
    public Sprite sprite1, sprite2, sprite3;
    public float hp;

    public ParticleSystem vfx_BlockBroken;

    //bool isQuitting = false;

    private void Awake()
    {
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSprite()
    {
        if (hp <= 0)
        {
            Instantiate(vfx_BlockBroken, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (hp <= 1) sprRen.sprite = sprite1;
        else if (hp <= 2) sprRen.sprite = sprite2;
        else if (hp <= 3) sprRen.sprite = sprite3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            hp-=1;
            ChangeSprite();
            anim.Play("Hit");
        }
        if (collision.tag == "SmallAttack")
        {
            hp -= 0.5f;
            ChangeSprite();
            anim.Play("Hit");
        }
        if (collision.tag == "EnemyAttack")
        {
            hp -= 3f;
            ChangeSprite();
            anim.Play("Hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            hp-=1;
            ChangeSprite();
            anim.Play("Hit");
        }
        if (collision.gameObject.tag == "SmallAttack")
        {
            hp -= 0.5f;
            ChangeSprite();
            anim.Play("Hit");
        }
        if (collision.gameObject.tag == "EnemyAttack")
        {
            hp -= 3f;
            ChangeSprite();
            anim.Play("Hit");
        }
    }
    //void OnApplicationQuit()
    //{
    //    isQuitting = true;
    //}
    //private void OnDestroy()
    //{
    //    if (!isQuitting)
    //        Instantiate(vfx_BlockBroken, transform.position, Quaternion.identity);
    //}
}
