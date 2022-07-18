using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject back, head, player;
    Back backScr;
    Head headScr;
    Player playerScr;


    public float maxSize;
    float maxSizeDefault;
    float xSizeStretch; //kich co da duoc stretch ngay khi ngung stretch
    public bool isStretch; //kiem tra dang keo dan khong
    public bool isDoneShrink; //kiem tra da co lai hoan toan chua

    public AudioClip stretchClip, shrinkClip;
    AudioSource music;

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        music = gameObject.GetComponent<AudioSource>();
        playerScr = player.GetComponent<Player>();
        headScr = head.GetComponent<Head>();
        backScr = back.GetComponent<Back>();
    }
    // Start is called before the first frame update
    void Start()
    {
        music.clip = shrinkClip;
        isStretch = false;
        isDoneShrink = true;
        maxSizeDefault = maxSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && isDoneShrink)
        {
            isStretch = true;
            isDoneShrink = false;
        }
        if (Input.GetKeyUp(KeyCode.M)&& isStretch == true)
        {
            xSizeStretch = transform.localScale.x;

            isStretch = false;
        }

        if (isStretch) Stretch();
        else if (isDoneShrink == false) Shrink();



        if (transform.localScale.x == 0 && isStretch == false)
        {
            if (isDoneShrink == false)
            {
                isDoneShrink = true;

                if (playerScr.isTurnRight)
                    player.transform.Translate(new Vector3(transform.localPosition.x, 0, 0));
                else player.transform.Translate(new Vector3(-transform.localPosition.x, 0, 0));
                //backScr.FixPos();

                transform.localPosition = Vector3.zero;              
            }
        }

    }

    public void DoLandAnim()
    {
        StartCoroutine(LandAnim());
        //transform.localScale = new Vector3(transform.localScale.x)
    }
    IEnumerator LandAnim()
    {
        for (int i = 0; i <= 7; i++)
        {
            transform.localScale += new Vector3(0f, -0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i <= 7; i++)
        {
            transform.localScale += new Vector3(0f, 0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Stretch()
    {
        //print(music.clip == stretchClip);
        if (/*music.isPlaying == false*/music.clip.name == "Shrink")
        {
            music.clip = stretchClip;
            music.Play();
        }
        isStretch = true;

        if (headScr.isHaveObstacleX()) maxSize = transform.localScale.x;
        else maxSize = maxSizeDefault;

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxSize, transform.localScale.y, 1), Time.deltaTime * 3);

    }

    void Shrink()
    {
        if (/*music.isPlaying == false && */music.clip.name == "Stretch")
        {
            music.clip = shrinkClip;
            music.Play();
        }
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, transform.localScale.y, 1), Time.deltaTime * 5);
            if (transform.localScale.x <= 0.05f)
                transform.localScale = new Vector3(0, transform.localScale.y, 1);

            transform.localPosition = new Vector3(xSizeStretch - transform.localScale.x, 0);
    }
}
