using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public Player otherPlayerScr;
    public GameObject blade, smallBlade;

    public GameObject back, head, body;
    Back backScr;
    Head headScr;
    Body bodyScr;

    public float speed, jumpForce, acsimetForce;
    float xdir, ydir, xSpeed, ySpeed, defaultGravity;

    public bool isTurnRight; // kiem tra xem co dang xoay mat ve phia ben phai khong
    public bool isWalking, isLying, isAttacking, isCanMove, isOnWaterSurface, isBlinking;

    public float timeJumpMax, timeJump, timeBlinking;
    public static float hp, maxHp;

    public bool isReverseX, isReverseY;

    public HPSlider hpSliderScr;
    public ParticleSystem vfx_Die;

    public AudioClip JumpClip;
    AudioSource music;
    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        backScr = back.GetComponent<Back>();
        headScr = head.GetComponent<Head>();
        bodyScr = body.GetComponent<Body>();
        maxHp = 3;
        hp = maxHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        isTurnRight = true;
        defaultGravity = rb.gravityScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReverseX)
        {
            xdir = -Input.GetAxisRaw("Horizontal");
        }
        else
        {
            xdir = Input.GetAxisRaw("Horizontal");
        }
        ydir = Input.GetAxisRaw("Vertical");

        xSpeed = rb.velocity.x;
        ySpeed = rb.velocity.y;

        anim.SetFloat("xSpeed", xSpeed);
        anim.SetFloat("ySpeed", ySpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnWaterSurface)
            {
                music.clip = JumpClip;
                music.Play();
                timeJump = timeJumpMax * 0.75f;
            }
            if (isGround())
            {
                music.clip = JumpClip;
                music.Play();
                timeJump = timeJumpMax;
            }          
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isAttacking == false) Attack();
        }

        // ko cho quay dau khi dang keo dan
        if (bodyScr.isDoneShrink == true && xdir != 0) 
        {
            if (xdir > 0 && isTurnRight == false)
            {
                transform.localScale = new Vector2(1, 1);
                isTurnRight = true;
            }
            else if (xdir < 0 && isTurnRight == true)
            {
                transform.localScale = new Vector2(-1, 1);
                isTurnRight = false;
            }
        }

        if (xdir != 0) isWalking = true;
        else isWalking = false;

        if (ydir < 0) isLying = true;
        else
        {
            if (isHaveObstacleAbove() == false) isLying = false; // new van co vat can phia tren dau sau khi nam thi khong duoc dung day
        }

        FixStuckWalking();

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGround", isGround());
        anim.SetBool("isLying", isLying);
    }

    private void FixedUpdate()
    {
        if (timeJump >= 0)
        {
            if (isLying)
                timeJump -= Time.fixedDeltaTime * 1.3f;
            else
                timeJump -= Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        if (isCanMove) Move();
    }

    void FixStuckWalking()
    {
        if (xdir != 0 && xSpeed == 0 && ySpeed == 0 && headScr.isHaveObstacleX() == false)
        {
            if (isReverseY == false) rb.AddForce(Vector2.up * 30);
            else rb.AddForce(Vector2.up * -30);
        }
    }
    void Move()
    {
        if (isLying || bodyScr.isStretch)
        {
            rb.AddForce(new Vector3(xdir, 0, 0) * speed/1.7f);
        }
        else
        {
            rb.AddForce(new Vector3(xdir, 0, 0) * speed);
        }
    }

    void Jump()
    {
        if (timeJump > 0)
        {
            if (isLying == false)
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            else
                rb.velocity = new Vector3(rb.velocity.x, jumpForce*0.8f);
        }
    }

    void Attack()
    {
        int reverseVar;
        if (isReverseY) reverseVar = -1;
        else reverseVar = 1;
        if (isLying)
        {
            anim.Play("DownAttack");
            if (isTurnRight)
                Instantiate(smallBlade, head.transform.position + new Vector3(0, 0.27f * reverseVar, 0), Quaternion.identity);
            else
                Instantiate(smallBlade, head.transform.position + new Vector3(0, 0.27f * reverseVar, 0), Quaternion.Euler(new Vector3(0,0,180)));
        }
        else
        {
            anim.Play("Attack");
            if (isTurnRight)
                Instantiate(blade, head.transform.position + new Vector3(0f, 0.5f * reverseVar, 0), Quaternion.identity);
            else
                Instantiate(blade, head.transform.position + new Vector3(0f, 0.5f * reverseVar, 0), Quaternion.Euler(new Vector3(0, 0, 180)));

        }
    }

    bool isGround()
    {
        return backScr.isGround() || headScr.isGround();
    }

    bool isHaveObstacleAbove()
    {
        return backScr.isHaveObstacleAbove() || headScr.isHaveObstacleAbove();
    }

    void Hurt()
    {
        isBlinking = true;
        if (isLying) anim.Play("HurtLie");
        else anim.Play("Hurt");
        anim.SetBool("isBlinking", isBlinking);
        Invoke("EndBlinking", timeBlinking);

        if (hp<=0)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    void EndBlinking()
    {
        isBlinking = false;
        anim.SetBool("isBlinking", isBlinking);
    }

    void LandAnim()
    {
        headScr.DoLandAnim();
        backScr.DoLandAnim();
        bodyScr.DoLandAnim();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            rb.AddForce(Vector3.up * acsimetForce);
        }
        if (collision.tag == "WaterSurface")
        {
            isOnWaterSurface = true;
        }
        if ((collision.tag == "EnemyAttack" || collision.tag == "Enemy") 
            && (collision.gameObject.GetComponent<Enemy>() == null || collision.gameObject.GetComponent<Enemy>().HP > 0) && isBlinking == false)
        {
            rb.velocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized * 7;
            CameraController.Shake();
            hp--;
            hpSliderScr.DecreaseHP();
            Hurt();
            otherPlayerScr.Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Enemy" && (collision.gameObject.GetComponent<Enemy>() == null || collision.gameObject.GetComponent<Enemy>().HP > 0) && isBlinking == false)
        //{
        //    rb.velocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized * 7;
        //    //print(rb.velocity);
        //    CameraController.Shake();
        //    hp--;
        //    hpSliderScr.DecreaseHP();
        //    Hurt();
        //    otherPlayerScr.Hurt();
        //}
        if (collision.tag == "Water")
        {
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            //print("ExitWater");
            rb.gravityScale = defaultGravity;
        }
        if (collision.tag == "WaterSurface")
        {
            isOnWaterSurface = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && (collision.gameObject.GetComponent<Enemy>() == null || collision.gameObject.GetComponent<Enemy>().HP > 0) && isBlinking == false)
        {
            rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 7;
            CameraController.Shake();
            hp--;
            hpSliderScr.DecreaseHP();
            Hurt();
            otherPlayerScr.Hurt();
        }
        if (collision.contacts[0].normal.y < -0.9 && collision.gameObject.CompareTag("Ground")) timeJump = timeJumpMax * 0.1f;
        if (isGround()) //Landing (Bat dau tiep dat)
        {
            //Instantiate(vfx_Landing, transform.position, Quaternion.identity);
            LandAnim();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy" && (collision.gameObject.GetComponent<Enemy>()==null || collision.gameObject.GetComponent<Enemy>().HP > 0) && isBlinking == false)
        {
            rb.velocity = new Vector3(collision.contacts[0].normal.x * 1000, collision.contacts[0].normal.y * 1000, 0).normalized * 7;
            CameraController.Shake();
            hp--;
            hpSliderScr.DecreaseHP();
            Hurt();
            otherPlayerScr.Hurt();
        }
    }

    private void OnDestroy()
    {
        Instantiate(vfx_Die, transform.position, Quaternion.identity);
    }
}
