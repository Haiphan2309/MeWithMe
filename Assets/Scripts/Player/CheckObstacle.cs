using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    public bool isHaveObstacle;
    // Start is called before the first frame update
    void Start()
    {
        isHaveObstacle = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHaveObstacle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHaveObstacle = false;
    }
}
