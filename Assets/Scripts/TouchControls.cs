using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TouchControls : MonoBehaviour
{

    public PaintGun gun;
    private Vector2 firstPos;
    private Vector2 lastPos;

    public float SWIPE_THRESHOLD = 20f;

    void Start() {
        if(SystemInfo.deviceType != DeviceType.Handheld){
            GetComponent<TouchControls>().enabled = false;
        }
    }

     void Update () {   

        foreach (Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began){
                Debug.Log("BEGIN");
                firstPos = touch.position;
            } 
            else if(touch.phase == TouchPhase.Moved){
                lastPos = touch.position;
            } 
            else if(touch.phase == TouchPhase.Ended){
                lastPos = touch.position;
                checkSwipe();
            }
        }


     }

      void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (firstPos.y - lastPos.y > 0)//Down swipe
            {
                Debug.Log("OTHER: Down");
            }
            else if (firstPos.y - lastPos.y < 0)//Up swipe
            {
                Debug.Log("OTHER: Up");
            }
            lastPos = firstPos;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (firstPos.x - lastPos.x > 0)//Left swipe
            {
                Debug.Log("Swipe: Left");
                gun.changeColor(-1);
            }
            else if (firstPos.x - lastPos.x < 0)//Right swipe
            {
                Debug.Log("Swipe: Right");
                gun.changeColor(1);
            }
            lastPos = firstPos;
        }
        //Check if a Tap
        else {
            gun.shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("OTHER: Tap");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(firstPos.y - lastPos.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(firstPos.x - lastPos.x);
    }

}
