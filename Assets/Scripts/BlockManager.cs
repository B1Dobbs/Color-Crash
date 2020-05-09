using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Palette;

public class BlockManager : MonoBehaviour
{
    List<ColorName> available = new List<ColorName>();

    public GameObject block;

    public GameObject spawnstart;

    Palette palette;

    LevelInterface level;

    public struct Coords
    {
        public Coords(double x, double y){ X = x; Y = y;}
        public double X { get; }
        public double Y { get; }

    }

    SortedDictionary<Coords, Block> blocks = new SortedDictionary<Coords, Block>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLevel(LevelInterface level){
        this.level = level;
    }

    public void spawnRow(){

        // print(block.GetComponent<MeshRenderer>().bounds.size);
        float width = block.GetComponent<MeshRenderer>().bounds.size.x;

        for(int i = 0; i < 7; i++){
            float x = spawnstart.transform.position.x + ((i + 1) * width);
            // print(x);
            spawnBlock(new Vector3(x, spawnstart.transform.position.y, spawnstart.transform.position.z), new Coords(0, i));
        }
    }

    void spawnBlock(Vector3 location, Coords coords){
        Block blockInstance = Instantiate(block, location, Quaternion.identity).GetComponent<Block>();
        //blocks.Add(coords, blockInstance);

        //blockInstance.initialize(level.getAvailable()); 

    }

    // void OnTriggerExit(Collider other){
    //     print("Exit Trigger");

    //     if(other.gameObject.tag == "Block"){
    //         spawnRow();
    //     }

    // }
}
