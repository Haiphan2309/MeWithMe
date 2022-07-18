using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMon : MonoBehaviour
{
    public PlantHead plantHeadScr;
    public float upDistance, speed, timeGrown;
    Vector3 upPos, initPos;
    public bool isUp, isPlayerNear;
    public float direct; //0: up, 1:right, 2: down, 3: left
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        if (direct == 0) upPos = initPos + new Vector3(0,upDistance,0);
        if (direct == 2) upPos = initPos + new Vector3(0, -upDistance, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (plantHeadScr.isPlayerNear())
        {
            if (isPlayerNear == false)
            {
                isPlayerNear = true;
                InvokeRepeating("UpDown", 0, timeGrown);
            }
        }
        else
        {
            CancelInvoke();
            isPlayerNear = false;
            isUp = false;
        }

        if (isUp)
        {
            if (Vector3.Magnitude(transform.position - upPos) > 0.05f)
            {
                Up();
            }
        }
        else
        {
            if (Vector3.Magnitude(transform.position - initPos) > 0.05f)
            {
                Down();
            }
        }
    }

    void UpDown()
    {
        isUp = !isUp;
    }

    void Up()
    {
        transform.position = Vector3.Lerp(transform.position, upPos, speed * Time.deltaTime);
    }

    void Down()
    {
        transform.position = Vector3.Lerp(transform.position, initPos, speed * Time.deltaTime);
    }
}
