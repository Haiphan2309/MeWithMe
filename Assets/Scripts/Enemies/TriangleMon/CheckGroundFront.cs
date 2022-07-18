using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundFront : MonoBehaviour
{
    public LayerMask groundLayer;
    Collider2D coll;

    public bool isGroundFront;
    //public string tagCheck;


    private void Awake()
    {
        coll = gameObject.GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isGroundFront = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(groundLayer)) isGroundFront = true;
        else isGroundFront = false;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (tagCheck != "EveryThing")
    //    {
    //        if (collision.tag == tagCheck) isGroundFront = true;
    //    }
    //    else
    //    {
    //        if (collision.tag != "Player" && collision.tag != "EditorOnly")
    //            isGroundFront = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (tagCheck != "EveryThing")
    //    {
    //        if (collision.tag == tagCheck) isGroundFront = false;
    //    }
    //    else
    //    {
    //        if (collision.tag != "Player" && collision.tag != "EditorOnly")
    //            isGroundFront = false;
    //    }
    //}
}
