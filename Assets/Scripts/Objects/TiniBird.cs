using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiniBird : MonoBehaviour
{
    float x0, y0, xTarget, yTarget;
    Animator anim;

    public float speed;
    AudioSource music;

    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        x0 = transform.position.x;
        y0 = transform.position.y;
        xTarget = x0;
        yTarget = y0;
        Invoke("SetRandomPos", 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void SetRandomPos()
    {
        anim.Play("Move");
        xTarget = x0 + Random.Range(-5, 5) / 10f;
        if (xTarget < x0) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);

        Invoke("SetRandomPos", Random.Range(5, 10) / 5f);
    }

    void SetFlyPos()
    {
        music.Play();
        speed = 0.75f;
        anim.Play("Flying");
        yTarget = y0 + 20;
        xTarget = x0 + Random.Range(-12, 12);

        if (xTarget < x0) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(xTarget, yTarget, 0), speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Attack") || collision.CompareTag("SmallAttack"))
        {           
            SetFlyPos();
            Destroy(gameObject, 3);
        }
    }
}
