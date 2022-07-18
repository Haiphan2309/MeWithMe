using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcsimetForce : MonoBehaviour
{
    public float acsimetForce;
    float defaultGravity;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultGravity = rb.gravityScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            rb.AddForce(Vector3.up * acsimetForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            rb.gravityScale = defaultGravity;
        }
    }
}
