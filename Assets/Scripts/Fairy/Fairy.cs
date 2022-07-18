using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    public GameObject target;
    public Player playerScr;

    public SpriteRenderer sprRen;

    public float speed;
    Vector3 targetPos;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget(target);

        if (playerScr != null)
        {
            if (playerScr.isTurnRight) sprRen.flipX = false;
            else sprRen.flipX = true;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    void FollowTarget(GameObject obj)
    {
        targetPos = obj.transform.position + Vector3.up * 1.5f;
    }
}
