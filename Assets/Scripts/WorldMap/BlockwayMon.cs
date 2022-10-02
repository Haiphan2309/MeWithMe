using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockwayMon : MonoBehaviour
{
    public GameObject dieObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dieObj.SetActive(true);
        transform.localScale = Vector2.zero;
    }
}
