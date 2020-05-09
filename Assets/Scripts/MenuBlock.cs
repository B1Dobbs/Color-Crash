using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Palette;

public class MenuBlock : MonoBehaviour
{
    Color outerColor;

    Color currColor;

    Color fadeColor;

    public float fadeTime = 2;
    public float fadeStart = 0;


    // Start is called before the first frame update
    void Start()
    {
        outerColor = GetComponent<MeshRenderer>().material.color;
        fadeTime = Random.Range(0.5f, 3.0f);
        setColor();
        setNextColor();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStart < fadeTime)
        {
            fadeStart += Time.deltaTime / fadeTime;
            fade();
        }


        print(fadeTime);
        
    }

    public void setColor(){

        float willChange = Random.Range(0.0f, 1.0f);
        print("willChange: " + willChange);
        if(willChange < 0.9){
            currColor = outerColor;
            GetComponent<MeshRenderer>().material.color = outerColor;
        } else{
            Color randColor = Palette.getColorValue((ColorName) Random.Range(0, colorValues.Count - 1));
            GetComponent<MeshRenderer>().material.color = randColor;
            currColor = randColor;
        }

    }

    public void setNextColor(){

        float willChange = Random.Range(0.0f, 1.0f);
        print("willChange");
        currColor = GetComponent<MeshRenderer>().material.color;
        if(willChange < 0.9){
            fadeColor = outerColor;
        } else{
            fadeColor = Palette.getColorValue((ColorName) Random.Range(0, colorValues.Count - 1));
        }

    }

    void fade(){

        print("Fading");

        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(currColor, fadeColor, fadeStart);

       if(fadeStart >= fadeTime){
           fadeStart = 0.0f;
           setNextColor();
       }

    }
}
