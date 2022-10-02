using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    GameController gameControllerScr;
    public GameObject fairy;
    public ParticleSystem vfx_BlockBroken;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("GameController")!= null)
            gameControllerScr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") || collision.CompareTag("SmallAttack"))
        {
            Instantiate(vfx_BlockBroken, transform.position, Quaternion.identity);
            gameControllerScr.isGetFairy = true;
            fairy.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") || collision.gameObject.CompareTag("SmallAttack"))
        {
            Instantiate(vfx_BlockBroken, transform.position, Quaternion.identity);
            gameControllerScr.isGetFairy = true;
            fairy.SetActive(true);
            Destroy(gameObject);
        }
    }

    //bool isQuitting = false;
    //void OnApplicationQuit()
    //{
    //    isQuitting = true;
    //}
    //private void OnDestroy()
    //{
    //    if (!isQuitting && GameObject.FindGameObjectWithTag("GameController") != null)
    //    {
    //        Instantiate(vfx_BlockBroken, transform.position, Quaternion.identity);
    //        gameControllerScr.isGetFairy = true;
    //        fairy.SetActive(true);
    //    }
    //}
}
