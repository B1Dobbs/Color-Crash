using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Palette.ColorName;
using static Palette;
using TMPro;

public class PaintGun : MonoBehaviour
{

    public GameObject paint;

    public GameObject spawnLoc;
    public GameObject cannon;

    public TextMeshProUGUI textPro;

    float targetTime = 0.0f;

    LevelInterface level;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 
        if (targetTime > 0.0f)
        {
            targetTime -= Time.deltaTime;
        }
        
    }

    public void setLevel(LevelInterface level){
        this.level = level;
    }

    public void setup(){
        ColorName currColor = level.getPalette().getCurrColor();
        Color newColor = Palette.getColorValue(currColor);
        cannon.GetComponent<MeshRenderer>().material.color = newColor;
        textPro.text = currColor.ToString();
        textPro.color = newColor;
    }

    public void shoot(Vector3 target){

        //Debug.Log("TARGET: " + direction);
        // Debug.Log("Origin: " + spawnLoc.transform.position);

        if(targetTime <= 0.0f){
            targetTime = 0.1f;
            transform.eulerAngles = new Vector3(0, 180, getCannonAngle(target)); 

            GameObject paintInstance = Instantiate(paint, spawnLoc.transform.position, Quaternion.identity);
            paintInstance.GetComponent<Paint>().setColor(level.getPalette().getCurrColor());

            Vector3 paintDirection = target - paintInstance.transform.position;
            paintDirection = paintDirection.normalized;
            paintInstance.GetComponent<Rigidbody>().AddForce(paintDirection * 800f);
            transform.Rotate (new Vector3 (0, 0, paintDirection.z) * Time.deltaTime);
           
        }
    }

    float getCannonAngle(Vector3 target){
        //Account for the cannon being below the origin point
        float relativeY = target.y - transform.position.y;

        // Create a right angle between target and cannon where C is the hypotenuse.
        double c = Math.Sqrt((relativeY * relativeY) + (target.x * target.x));

        //Find the angle between c and y, convert radians to degrees
        float angle = (180 * Mathf.Acos(relativeY / (float) c)) / Mathf.PI;
        
        //Find the correct x direction
        float rotation = angle * (target.x / Math.Abs(target.x));

        return rotation;

    }

    public void changeColor(int direction){
        // Debug.Log(getPalette().changeColor(direction));
        level.getPalette().changeColor(direction);
        ColorName currColor = level.getPalette().getCurrColor();
        Color newColor = Palette.getColorValue(currColor);
        cannon.GetComponent<MeshRenderer>().material.color = newColor;
        textPro.text = currColor.ToString();
        textPro.color = newColor;

    }
}
