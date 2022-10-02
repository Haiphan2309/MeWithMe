using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    GameController gameControllerScr;

    public GameObject secondGate;
    Animator anim;
    public bool isPlayerEnter;

    private void Awake()
    {
        gameControllerScr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print(collision.gameObject);
        if (collision.transform.parent.CompareTag("Player"))
        {
            isPlayerEnter = true;
            anim.SetBool("isPlayerIn", isPlayerEnter);
            collision.transform.parent.SetParent(gameObject.transform);
            if (secondGate != null)
            {
                if (secondGate.GetComponent<Gate>().isPlayerEnter) NextLevel();
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("ExitGate");
        //print(collision.gameObject);
        if (collision.transform.parent.CompareTag("Player"))
        {
            isPlayerEnter = false;
            anim.SetBool("isPlayerIn", isPlayerEnter);
            collision.transform.parent.SetParent(null);
        }
    }
    void NextLevel()
    {
        Time.timeScale = 0.2f;
        anim.Play("PlayerEnter");
        secondGate.GetComponent<Animator>().Play("PlayerEnter");
        gameControllerScr.ToWorldMap(1);
    }
}
