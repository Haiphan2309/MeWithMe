using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : Enemy
{
    public GameObject parentObject;
    public float distanceDetect, speedFollow;
    public bool isFollowPlayer;

    Vector3 centerPos;
    // Start is called before the first frame update
    void Start()
    {
        centerPos = transform.position - new Vector3(0, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowPlayer && isDie == false)
        {
            FollowPlayer();
        }

        if (HP <= 0 && parentObject != null)
        {           
            gameObject.transform.SetParent(null);
            Destroy(parentObject);
        }
        anim.SetBool("isPlayerNear", isPlayerNear());
    }

    void FollowPlayer()
    {
        transform.up = Vector3.Lerp(transform.up, player.transform.position - transform.position, 5 * Time.deltaTime);
        if (Vector3.Magnitude(player.transform.position-centerPos)<5f)
            transform.position = Vector3.Lerp(transform.position, player.transform.position, speedFollow * Time.deltaTime);
    }
    public bool isPlayerNear()
    {
        if (player != null)
        {
            if (Vector3.Magnitude(transform.position - player.transform.position) <= distanceDetect) return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack") GetHit(1);
        if (collision.tag == "SmallAttack") GetHit(0.5f);
        if (collision.tag == "EnemyAttack") GetHit(3);
    }
}
