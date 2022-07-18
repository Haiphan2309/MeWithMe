using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattle : MonoBehaviour
{
    public GameObject blackPlayer, blockWay, kaSlime;
    CameraController cameraScr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void KaslimeAppear()
    {
        kaSlime.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            blockWay.SetActive(true);
            blackPlayer.SetActive(true);
            Invoke("KaslimeAppear", 1.3f);
            cameraScr = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            cameraScr.isFollow2Player = true;
            cameraScr.minX = 8;
            cameraScr.maxX = 10;
            cameraScr.minY = 6;
            cameraScr.minSize = 9.5f;

            gameObject.SetActive(false);
        }
    }
}
