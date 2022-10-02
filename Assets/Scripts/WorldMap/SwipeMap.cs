using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMap : MonoBehaviour
{
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    [SerializeField] private CameraController cameraSCr;
    void Start()
    {
        dragDistance = Screen.height * 5 / 100; //dragDistance is 2% height of the screen
    }

    void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
                if (Mathf.Abs(lp.x - fp.x) > dragDistance)
                {
                    cameraSCr.isSwipeMap = true;
                    cameraSCr.switeMovePos = Camera.main.ScreenToWorldPoint(fp).x - Camera.main.ScreenToWorldPoint(lp).x;
                }
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                cameraSCr.isSwipeMap = false;
            }
        }
    }
}
