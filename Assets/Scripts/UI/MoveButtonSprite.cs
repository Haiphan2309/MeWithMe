using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonSprite : MonoBehaviour
{
    Vector3 touchPos;
    [SerializeField] Image upImage, downImage, leftImage, rightImage;
    [SerializeField] Sprite spriteBtnIdle, spriteBtnPress;

    [SerializeField] float horizontalLimit, verticalLimit;

    bool isFirstTouch = false, isSecondTouch = false;

    public void MoveBtnDown()
    {
        if (Input.touchCount > 0)
        {

            Touch touch;
            if (Input.touchCount == 1)
            {
                isFirstTouch = true;
                touch = Input.GetTouch(0);
            }
            else if (Input.touchCount == 2)
            {
                if (isFirstTouch == true) touch = Input.GetTouch(0);
                else
                {
                    isSecondTouch = true;
                    touch = Input.GetTouch(1);
                }
            }
            else
            {
                if (isFirstTouch == true) touch = Input.GetTouch(0);
                else if (isSecondTouch == true) touch = Input.GetTouch(1);
                else touch = Input.GetTouch(2);
            }
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touchPos.x - transform.position.x > horizontalLimit)
            {
                rightImage.sprite = spriteBtnPress;
                leftImage.sprite = spriteBtnIdle;
            }
            else if (touchPos.x - transform.position.x < -horizontalLimit)
            {
                leftImage.sprite = spriteBtnPress;
                rightImage.sprite = spriteBtnIdle;
            }
            else
            {
                rightImage.sprite = spriteBtnIdle;
                leftImage.sprite = spriteBtnIdle;
            }
            if (touchPos.y - transform.position.y < -verticalLimit)
            {
                downImage.sprite = spriteBtnPress;
                upImage.sprite = spriteBtnIdle;
            }
            else if (touchPos.y - transform.position.y > verticalLimit)
            {
                upImage.sprite = spriteBtnPress;
                downImage.sprite = spriteBtnIdle;
            }
            else
            {
                upImage.sprite = spriteBtnIdle;
                downImage.sprite = spriteBtnIdle;
            }
        }
    }
    public void MoveBtnUp()
    {
        isFirstTouch = false;
        isSecondTouch = false;

        rightImage.sprite = spriteBtnIdle;
        leftImage.sprite = spriteBtnIdle;
        upImage.sprite = spriteBtnIdle;
        downImage.sprite = spriteBtnIdle;
    }
}
