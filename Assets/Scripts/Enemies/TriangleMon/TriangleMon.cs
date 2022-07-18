using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMon : Enemy
{
    public CheckGroundFront checkScr, checkForwalkScr;
    public bool isMoveRight;
    public bool isMoving;

    public float speed;

    float xRotate;
    // Start is called before the first frame update
    void Start()
    {
        xRotate = transform.rotation.x * 180; //transform.rotation.x = 1 khi bi dao nguoc, con khong dao nguoc thi = 0
        InvokeRepeating("LookAround", 3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) CancelInvoke();
        if (checkScr.isGroundFront == false)
        {
            isMoveRight = !isMoveRight;
        }
        if (isMoveRight) transform.rotation = Quaternion.Euler(xRotate, 180, 0); 
        else transform.rotation = Quaternion.Euler(xRotate, 0, 0);
    }

    void LookAround()
    {
        //isMoving = false;
        anim.Play("LookAround");
    }

    private void FixedUpdate()
    {
        if (isDie == false)
        {
            anim.SetBool("isMoving", isMoving);
            if (isMoving) Move();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack") GetHit(3);
    }
}
