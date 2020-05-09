using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class KeyBoardControls : MonoBehaviour
{

    public GameObject textGameObject; 

    public PaintGun gun;

    void Start() {
        if(SystemInfo.deviceType != DeviceType.Desktop){
            GetComponent<KeyBoardControls>().enabled = false;
        }
    }

     void Update () {   

        if (Input.GetMouseButtonDown(0)){
            gun.shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            gun.changeColor(1);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            gun.changeColor(-1);
        }


     }

    
}
