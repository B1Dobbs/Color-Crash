using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Palette;

public class Survival : MonoBehaviour, LevelInterface
{
    int[] points = new int[Palette.colorValues.Count];
    int nextColorGoal = 25;
    BlockManager manager;
    Palette palette;

    public GameObject spawnLoc;
    public GameObject tower;

    float towerSize = 0;

    int addedColors = 0;

    // Start is called before the first frame update
    void Start()
    {
        palette = new Palette();
        palette.addAvailable(ColorName.Red);
        palette.addAvailable(ColorName.Blue);
        palette.addAvailable(ColorName.Yellow);
        // palette.addAvailable(ColorName.Green);
       // palette.addAvailable(ColorName.Purple);
        PaintGun gun = GameObject.Find("Gun").GetComponent<PaintGun>();
        gun.setLevel(this);
        gun.setup();

        towerSize = Instantiate(tower, spawnLoc.transform.position, Quaternion.identity).GetComponent<Collider>().bounds.size.y;

        for(int i = 0; i < points.Length; i++){
            points[i] = 0;
        }

        int test = 20;
        for(int i = 0; i < 13; i++){
            if( test < 150)
                test = 3 * test;
            else
                test += 100;
            
            // test = (20 * i) + 10;
            print(test);
        }
        
    }

    void getTop(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setManager(BlockManager manager){
        this.manager = manager;
    }

    public int[] getPoints(){
        return points;
    }

    public void addBreak(ColorName color){
        points[(int) color] += 1;

        if(getTotal() > nextColorGoal){
            addColor();
            if( nextColorGoal < 150)
                nextColorGoal = 2 * nextColorGoal;
            else
                nextColorGoal += 50;
            print("NEXT GOAL: " + nextColorGoal);
        }

    }

    public int getTotal(){
        int total = 0;
        for(int i = 0; i < points.Length; i++){
            total += points[i];
        }

        return total;
    }

    void OnTriggerEnter(Collider collider){
        print(collider.gameObject.tag);
        if(collider.gameObject.tag == "Block"){
            Block block = collider.gameObject.GetComponent<Block>();
             //if(block.isBroken == true){
                print("INITIALIZE");
                block.initialize(getAvailable(), true);
            //}

        }
    }

    void addColor(){
        if(addedColors == 0){
            //print("ADDING GREEN");
            palette.addAvailable(ColorName.Green);
            addedColors++;
        } else if (addedColors == 1){
            palette.addAvailable(ColorName.Orange);
            palette.addGunColor(ColorName.Green);
            addedColors++;
        } else if (addedColors == 2){
            palette.addAvailable(ColorName.Purple);
            palette.subAvailable(ColorName.Red);
            palette.addGunColor(ColorName.Orange);
            addedColors++;
        }else if (addedColors == 3){
            palette.addAvailable(ColorName.Vermillion);
            palette.subAvailable(ColorName.Yellow);
            palette.addGunColor(ColorName.Purple);
            addedColors++;
        } else if (addedColors == 4){
            palette.addAvailable(ColorName.Magenta);
            palette.subAvailable(ColorName.Blue);
            addedColors++;
        } else if (addedColors == 5){
            palette.addAvailable(ColorName.Chartreuse);
             palette.addGunColor(ColorName.Purple);
            addedColors++;
        } else if (addedColors == 6){
            palette.addAvailable(ColorName.Amber);
            addedColors++;
        } else if (addedColors == 7){
            palette.addAvailable(ColorName.Violet);
            addedColors++;
        } else if (addedColors == 8){
            palette.addAvailable(ColorName.Teal);
            addedColors++;
        }
        
    }

    public Palette getPalette(){
        return palette;
    }

    public List<ColorName> getAvailable(){
        return palette.getAvailable();
    }

    public float getRespawnOffset(){
        return towerSize;
    }
}
