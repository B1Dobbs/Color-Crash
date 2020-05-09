using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static Palette;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject endScreen;
    public GameObject level;
    public TextMeshProUGUI red;
    public TextMeshProUGUI blue;
    public TextMeshProUGUI yellow;
    public TextMeshProUGUI orange;
    public TextMeshProUGUI green;
    public TextMeshProUGUI purple;
    public TextMeshProUGUI vermillion;
    public TextMeshProUGUI amber;
    public TextMeshProUGUI chartreuse;
    public TextMeshProUGUI teal;
    public TextMeshProUGUI violet;
    public TextMeshProUGUI magenta;
    public TextMeshProUGUI total;

    public void ReloadMain(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void displayEnd(){
        endScreen.SetActive(true);

        int[] points = level.GetComponent<LevelInterface>().getPoints();

        red.text = points[(int) ColorName.Red].ToString();
        yellow.text = points[(int) ColorName.Yellow].ToString();
        blue.text = points[(int) ColorName.Blue].ToString();
        green.text = points[(int) ColorName.Green].ToString();
        orange.text = points[(int) ColorName.Orange].ToString();
        purple.text = points[(int) ColorName.Purple].ToString();
        vermillion.text = points[(int) ColorName.Vermillion].ToString();
        amber.text = points[(int) ColorName.Amber].ToString();
        chartreuse.text = points[(int) ColorName.Chartreuse].ToString();
        teal.text = points[(int) ColorName.Teal].ToString();
        violet.text = points[(int) ColorName.Violet].ToString();
        magenta.text = points[(int) ColorName.Magenta].ToString();

        int total = 0;
        for(int i = 0; i < points.Length; i++){
            total += points[i];
        }

        this.total.text = total.ToString();
        
    }

   void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Block"){
            
            Time.timeScale = 0f;
            displayEnd();
        }
    }
}
