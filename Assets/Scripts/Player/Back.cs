using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public LayerMask groundLayer;

    public GameObject player;
    Player playerScr;

    public Transform bodyTrans;
    float bodyXPos/*, bodyXScale*/;

    public float xSize, groundCheckSize;
    public ParticleSystem vfx_Landing, vfx_Walk;
    ParticleSystem walkPar;

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        playerScr = player.GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //xSize = transform.localScale.x;
        walkPar = Instantiate(vfx_Walk, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
            walkPar.transform.position = transform.position;
    }
    void LateUpdate()
    {       
        bodyXPos = bodyTrans.localPosition.x;
        //bodyXScale = bodyTrans.localScale.x;
        FixPos();
    }

    public void FixPos()
    {
        //if (playerScr.isTurnRight)
            transform.localPosition = new Vector2(bodyXPos - xSize / 2, transform.localPosition.y);
        //else transform.position = new Vector2(bodyXPos + xSize / 2, transform.position.y);
    }

    public void DoLandAnim()
    {
        if (isGround()) Instantiate(vfx_Landing, transform.position, Quaternion.identity);
        StartCoroutine(LandAnim());
    }
    IEnumerator LandAnim()
    {
        for (int i = 0; i <= 7; i++)
        {
            transform.localScale += new Vector3(0.01f, -0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i <= 7; i++)
        {
            transform.localScale += new Vector3(-0.01f, 0.01f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public bool isGround()
    {
        return Physics2D.OverlapCircle(transform.position, groundCheckSize, groundLayer);
    }

    public bool isHaveObstacleAbove()
    {
        if (playerScr.isReverseY == false)
        {
            if (playerScr.isTurnRight)
                return Physics2D.OverlapCircle(transform.position + new Vector3(-0.1f, 0.75f, 0), groundCheckSize / 1.5f, groundLayer);
            else
                return Physics2D.OverlapCircle(transform.position + new Vector3(0.1f, 0.75f, 0), groundCheckSize / 1.5f, groundLayer);
        }
        else
        {
            if (playerScr.isTurnRight)
                return Physics2D.OverlapCircle(transform.position + new Vector3(-0.1f, -0.75f, 0), groundCheckSize / 1.5f, groundLayer);
            else
                return Physics2D.OverlapCircle(transform.position + new Vector3(0.1f, -0.75f, 0), groundCheckSize / 1.5f, groundLayer);
        }
    }
    private void OnDrawGizmos()
    {
        //if (playerScr.isReverseY == false)
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, groundCheckSize);
        //    Gizmos.DrawWireSphere(transform.position + new Vector3(-0.1f, 0.75f, 0), groundCheckSize / 1.5f);
        //}
        //else
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(transform.position, groundCheckSize);
        //    Gizmos.DrawWireSphere(transform.position + new Vector3(-0.1f, -0.75f, 0), groundCheckSize / 1.5f);
        //}
    }
}
