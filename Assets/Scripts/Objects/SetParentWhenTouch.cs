using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentWhenTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.gameObject /*+ " " + collision.transform.parent*/);
        collision.gameObject.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("N");
        collision.gameObject.transform.SetParent(null);
    }
}
