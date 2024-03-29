using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBlock : MonoBehaviour
{
    SpriteRenderer sprRen;
    Animator anim;
    //public Sprite sprite1, sprite2, sprite3;
    public float hp;

    public ParticleSystem vfx_BlockBroken;

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
        if (hp <= 0) Destroy(gameObject);
        //else if (hp <= 1) sprRen.sprite = sprite1;
        //else if (hp <= 2) sprRen.sprite = sprite2;
        //else if (hp <= 3) sprRen.sprite = sprite3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack")
        {
            hp -= 3f;
            ChangeSprite();
            anim.Play("Hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            hp -= 3f;
            ChangeSprite();
            anim.Play("Hit");
        }
    }

    bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!isQuitting)
            Instantiate(vfx_BlockBroken, transform.position, Quaternion.identity);
    }
}
