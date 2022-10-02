using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueKaslime : Enemy
{
    public GameObject bullet, bossSlider, oldBossSlider;
    public Transform whitePlayer, blackPlayer;
    public float moveSpeed, timeNextMove, numOfBullet, angleGap;
    Vector3 targetPos;

    bool isEnd;

    public GameObject afterImage, whiteScene, credit;

    public AudioClip chargeClip;
    AudioSource music;
    public AudioSource bgMusic;

    public bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        Destroy(oldBossSlider,0.5f);
        bossSlider.SetActive(true);
        InvokeRepeating("RandomTargetPos", timeNextMove, timeNextMove);
        InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking) PlayChargeClip();
        anim.SetBool("isAttacking", isAttacking);
    }

    private void FixedUpdate()
    {
        if (HP<=0)
        {
            if (bgMusic.volume > 0) bgMusic.volume -= 0.01f;
        }
        if (HP <= 0 && isEnd == false)
        {
            isEnd = true;
            CancelInvoke();
            whiteScene.SetActive(true);
            CameraController.Shake();
            Time.timeScale = 0.1f;
            StartCoroutine(ResetTimeScale(3));
        }
        else
        {
            MoveToPos(targetPos);
        }
    }

    IEnumerator ResetTimeScale(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        Time.timeScale = 1;
    }

    void SetTrueIsAttacking()
    {
        isAttacking = true;
    }
    void InitCenterAttack()
    {
        anim.Play("TrueInitAttack");
        CancelInvoke();
        InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
        targetPos = new Vector3(Random.Range(0,18), 8, 0);
        Invoke("SetTrueIsAttacking",0f);
        Invoke("CenterAttack", 2);
    }
    void CenterAttack()
    {
        Vector3 target = transform.position - Vector3.up;
        for (int i = 1; i <= numOfBullet; i++)
        {
            GameObject bulletObj;
            bulletObj = Instantiate(bullet, transform.position + new Vector3(0,4.5f,0), Quaternion.identity);
            bulletObj.transform.up = target - new Vector3(0, 4.5f, 0) - transform.position;
            if (i % 2 == 0)
            {
                bulletObj.transform.rotation = Quaternion.Euler(0, 0, bulletObj.transform.rotation.eulerAngles.z + (i / 2 * angleGap));
            }
            else
            {
                //Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -(i/2 * 20) + 180));
                bulletObj.transform.rotation = Quaternion.Euler(0, 0, bulletObj.transform.rotation.eulerAngles.z - (i / 2 * angleGap));
            }
        }
        InvokeRepeating("RandomTargetPos", timeNextMove, timeNextMove);
        anim.Play("TrueIdle");
        isAttacking = false;
    }
    
    void InitNormalAttack(Transform targetTrans)
    {
        anim.Play("TrueInitAttack");
        CancelInvoke();
        InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
        Invoke("SetTrueIsAttacking", 0f);
        StartCoroutine(NormalAttack(targetTrans));
    }
    IEnumerator NormalAttack(Transform target)
    {
        yield return new WaitForSeconds(1);
        for (int i=0; i<3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject bulletObj;
            bulletObj = Instantiate(bullet, transform.position + new Vector3(0, 4.5f, 0), Quaternion.identity);
            bulletObj.transform.up = target.transform.position - new Vector3(0, 4.5f, 0) - transform.position;
        }
        InvokeRepeating("RandomTargetPos", timeNextMove, timeNextMove);
        InvokeRepeating("DoAfterImage", 0.1f, 0.1f);
        anim.Play("TrueIdle");
        isAttacking = false;
    }
    void DoAfterImage()
    {
        GameObject obj;
        obj = Instantiate(afterImage, transform.position, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = sprRen.sprite;
    }
    void RandomTargetPos()
    {
        targetPos = new Vector3(Random.Range(-4, 22), Random.Range(0, 7), 0);

        int rand = Random.Range(0, 11);
        print(rand);
        if (rand <= 2)
        {
            InitCenterAttack();
        }
        else if (rand<=4)
        {
            InitNormalAttack(whitePlayer);
        }
        else if (rand <=6)
        {
            InitNormalAttack(blackPlayer);
        }
    }

    void MoveToPos(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.fixedDeltaTime);
    }

    void PlayChargeClip()
    {
        if (music.isPlaying == false)
        {
            music.clip = chargeClip;
            music.Play();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!(collision.gameObject.tag == "Attack" || collision.gameObject.tag == "SmallAttack")) RandomTargetPos();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack") GetHit(1);
        if (collision.gameObject.tag == "SmallAttack") GetHit(0.5f);
    }

    private void OnDestroy()
    {
        credit.SetActive(true);
    }
}
