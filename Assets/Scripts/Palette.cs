using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Palette : MonoBehaviour
{

    int currColorIndex = 0;

    List<ColorName> available = new List<ColorName>();

    List<ColorName> gun = new List<ColorName>();

    public enum ColorName{
        White = -1,
        Red = 0,
        Vermillion = 1,
        Orange = 2,
        Amber = 3,
        Yellow = 4,
        Chartreuse = 5,
        Green = 6,
        Teal = 7,
        Blue = 8,
        Violet = 9,
        Purple = 10,
        Magenta = 11,

    }
     public static SortedDictionary<ColorName, Color> colorValues = new SortedDictionary<ColorName, Color>{
        { ColorName.Red, new Color(238, 32,40) },
        { ColorName.Vermillion, new Color(242, 83.25f, 39) },
        { ColorName.Orange, new Color(246, 134.5f, 38) },
        { ColorName.Amber, new Color(250, 185.75f, 37) },
        { ColorName.Yellow, new Color(254, 237, 36) },
        { ColorName.Chartreuse, new Color(127, 245.5f, 73.5f) },
        { ColorName.Green, new Color(0, 254, 111) },
        { ColorName.Teal, new Color(0, 188, 182.5f) },
        { ColorName.Blue, new Color(0, 122, 254) },
        { ColorName.Violet, new Color(71.5f, 61, 254) },
        { ColorName.Purple, new Color(143, 0 , 254) },
        { ColorName.Magenta, new Color(190.5f, 16, 147) },
    };
     public Palette(){

        currColorIndex = 2;

        //This will change once the paint store is added
        gun.Add(ColorName.Red);
        gun.Add(ColorName.Blue);
        gun.Add(ColorName.Yellow);
    }

    public static Color getWhite(){
        return new Color(255,255,255);
    }

    public void addAvailable(ColorName color){
        available.Add(color);
    }

    public void subAvailable(ColorName color){
        available.Remove(color);
    }

    public void addGunColor(ColorName color){
        gun.Add(color);
    }

    public void subGunColor(ColorName color){
        gun.Remove(color);
    }

    public static Color getColorValue(ColorName color){
        return ConvertColor(colorValues[color]);
    }

    public ColorName changeColor(int direction){

        int newIndex = currColorIndex + direction;

        if(newIndex < 0)
            newIndex = gun.Count - 1;
        else if(newIndex > gun.Count - 1)
            newIndex = 0;
        
        currColorIndex = newIndex;
        return gun[newIndex];

    }

    static ColorName getRelatedColor(ColorName color, int difference){
        int relatedIndex = (int) color + difference;
        int size = Enum.GetNames(typeof(ColorName)).Length - 2; //Accounts for White
        //Debug.Log("Size: " + size);
        
        if(relatedIndex < 0)
            relatedIndex += size;
        else if(relatedIndex > size)
            relatedIndex -= size + 1;

        return (ColorName) relatedIndex;
    }

    //Will return White if its not a color in the color wheel
    public static ColorName getSimpleMix(List<ColorName> colors){
        //These two mixes do not follow the RGB model
        if(colors.Contains(ColorName.Yellow) && colors.Contains(ColorName.Blue)){
            return ColorName.Green;
        }
        else if(colors.Contains(ColorName.Blue) && colors.Contains(ColorName.Red)){
            return ColorName.Purple;
        }

        return getColorName(((Color) colorValues[colors[0]] + (Color) colorValues[colors[1]]) / 2);

    }
    public static ColorName getColorName(Color color){
        //SortedDictionary<ColorName, Color> combined = primary + secondary + tertiary;
        foreach(ColorName key in colorValues.Keys) 
        { 
            Color found = (Color) colorValues[key];
            if(color.r == found.r && color.g == found.g && color.b == found.b)
                return key;
        } 

        return ColorName.White;

    }

    static Color ConvertColor (Color color) {
        return new Color(color.r/255.0f, color.g/255.0f, color.b/255.0f);
    }

    public ColorName getCurrColor(){
        return gun[currColorIndex];
    }

    public List<ColorName> getAvailable(){
        return available;
    }

    // public Color getCurrColorValue(){
    //     return getColorValue(getCurrColor());
    // }

    public static List<ColorName> getRequiredForColor(ColorName color){
        //Returns any primary colors
        if(color == ColorName.Red || color == ColorName.Blue || color == ColorName.Yellow){
            return new List<ColorName>{color};
        } 
        else if(color == ColorName.Orange || color == ColorName.Green || color == ColorName.Purple){
            ColorName left = getRelatedColor(color, -2);
            ColorName right = getRelatedColor(color, 2);
            return new List<ColorName>{left, right};
        }
        else {
            List<ColorName> list = new List<ColorName>();
            //Returns any tertiary colors as long as they're defined in order within ColorNames
            ColorName left = getRelatedColor(color, -1);
            ColorName right = getRelatedColor(color, 1);
            List<ColorName> drillLeft = getRequiredForColor(left);
            List<ColorName> drillRight = getRequiredForColor(right);
            // foreach(ColorName leftcolor in drillLeft){
            //     Debug.Log("LEFT: " + leftcolor.ToString());
            // }
            // foreach(ColorName rightcolor in drillRight){
            //     Debug.Log("RIGHT: " + rightcolor.ToString());
            // }
            if(drillLeft.Count > 1){
                list.Add(left);
                list.AddRange(drillLeft);
            } else {
                list.Add(right);
                list.AddRange(drillRight);
            }

            return list;
        }

    }

}
