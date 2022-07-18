using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Animator anim;
    public Transform whitePlayer, blackPlayer;
    public float maxX = 0, minX = 0, maxY = 0, minY = 0;
    public float moveSpeed, sizeSpeed;

    const float slideDistance = 20;
    public bool isFollow2Player;

    public float minSize = 6;

    public bool isScoll;
    //bool isScoll = false;

    Rigidbody2D rigi;
    Camera CameraScr;
    Vector3 target;
    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rigi = gameObject.GetComponent<Rigidbody2D>();
        CameraScr = gameObject.GetComponent<Camera>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isScoll == false)
        {
            // Tao camera di chuyen theo player
            target = transform.position;

            if (isFollow2Player) SetSize();
            FollowTarget();

            if (target.x < minX) target.x = minX;
            if (target.x > maxX) target.x = maxX;
            if (target.y < minY) target.y = minY;
            if (target.y > maxY) target.y = maxY;

            if (Time.timeScale != 0)
                transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime / Time.timeScale);
        }
    }

    private void FixedUpdate()
    {
        if (isScoll) Scoll();
    }

    float scollSpeed;
    void Scoll()
    {
        scollSpeed = Mathf.Lerp(scollSpeed, moveSpeed * Time.fixedDeltaTime, 0.4f * Time.fixedDeltaTime);
        transform.Translate(new Vector3(scollSpeed, 0, 0));
    }
    public Vector2 CameraPos
    {
        get { return transform.position; }
        set { 
                transform.position = value;
            transform.position -= new Vector3(0, 0, 10);
            }
    }

    void FollowTarget()
    {
        if (isFollow2Player == false)
        {
            if (whitePlayer != null)
            {
                target.x = whitePlayer.position.x;
                target.y = whitePlayer.position.y;
            }
        }
        else
        {
            if (whitePlayer != null && blackPlayer != null)
            {
                target.x = (whitePlayer.position.x + blackPlayer.transform.position.x) / 2;
                target.y = (whitePlayer.position.y + blackPlayer.transform.position.y) / 2;
            }
        }
    }

    void SetSize()
    {
        if (whitePlayer != null && blackPlayer != null)
        {
            float distanceX, distanceY;
            distanceX = Mathf.Abs(whitePlayer.transform.position.x - blackPlayer.transform.position.x);
            distanceY = Mathf.Abs(whitePlayer.transform.position.y - blackPlayer.transform.position.y);
            if (distanceX > distanceY && distanceX / 3 > minSize)
                CameraScr.orthographicSize = Mathf.Lerp(CameraScr.orthographicSize, distanceX / 3f, sizeSpeed * Time.deltaTime);
            else if (distanceX <= distanceY && distanceY / 2 + 3 > minSize)
                CameraScr.orthographicSize = Mathf.Lerp(CameraScr.orthographicSize, distanceY / 2 + 3, sizeSpeed * Time.deltaTime);
            else CameraScr.orthographicSize = Mathf.Lerp(CameraScr.orthographicSize, minSize, sizeSpeed * Time.deltaTime);
        }
    }

    public static void Shake() //rung
    {
        anim.Play("Shake");
    }

    public static void LightShake() //rung nhe
    {
        anim.Play("LightShake");
    }

    public static void SlideRight() //cuon phai
    {        
        anim.Play("SlideRight");
    }

    public static void SlideLeft()
    {
        anim.Play("SlideLeft");
    }

    public static void ZoomIn()
    {
        anim.Play("ZoomIn");
    }

    public static void ZoomOut()
    {
        anim.Play("ZoomOut");
    }

    public static void SuperZoomIn()
    {
        anim.Play("SuperZoomIn");
    }


    //public void Scoll()
    //{
    //    isScoll = true;
    //}
}
