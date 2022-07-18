using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTile : MonoBehaviour
{
    Animator anim;
    CompositeCollider2D coll;

    public LayerMask playerLayer;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        coll = gameObject.GetComponent<CompositeCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(playerLayer)) anim.Play("SecretAppear");
        else anim.Play("SecretDisappear");
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")) anim.Play("SecretAppear");
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")) anim.Play("SecretDisappear");
    //}
}
