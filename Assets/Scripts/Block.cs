using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Palette;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public ColorName innerColor;

    ColorName outerColor;

    bool hit = false;

    List<Block> neighbors;

    bool shouldMove = true;

    float moveTime = 2.0f;

    Vector3 startLoc;

    LevelInterface level;

    public GameObject respawn;

    SortedDictionary<ColorName, bool> required = new SortedDictionary<ColorName, bool>();
    void Start()
    {
        innerColor = ColorName.White;
        startLoc = transform.position;
        // print("START: " + startLoc);
        // this.setColor(ColorName.Magenta);
        //print(GetComponent<MeshRenderer>().bounds.size);
        // foreach(ColorName color in required.Keys){
        //     //Debug.Log("REQUIRED: " + color.ToString());
        // }

        level = GameObject.Find("Scripts").GetComponent<LevelInterface>();
        initialize(level.getAvailable(), false);
        //initialize(new List<ColorName>{ColorName.Green});

    
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTime > 0.0f && shouldMove)
        {
            moveTime -= Time.deltaTime;
            move();
        }

    }

    void getNeighbors(){
        neighbors = new List<Block>();
        Vector3[] directions = new Vector3[4]{
            transform.TransformDirection(Vector3.left),
            transform.TransformDirection(Vector3.right), 
            transform.TransformDirection(Vector3.up),
            transform.TransformDirection(Vector3.down)
        };

        for(int i = 0; i < directions.Length; i++){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, directions[i], out hit, 1.5f))
            {   
                if(hit.collider.gameObject.tag == "Block"){
                    Block neighbor = hit.collider.gameObject.GetComponent<Block>();
                    if(neighbor.getColor() == getColor())
                        neighbors.Add(neighbor);
                    //print("NEIGHBOR: " + hit.collider.gameObject.GetComponent<Block>().getColor());
                }
                //print("NEIGHBOR: " + hit.collider.gameObject.GetComponent<Block>().getColor());
                print("Hit: " + hit.collider.gameObject.name);
            }
        }
        
    }

    void move(){
        if(moveTime <= 0.0f){
            // print("Moving");
            moveTime = 2.0f;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }

    public void initialize(List<ColorName> available, bool respawn){
        hit = false;
        if(respawn){
            StartCoroutine(WaitForNeighbor(10.0f));
        } else{
             StartCoroutine(WaitForNeighbor(0.1f));
        }
        setColor(available[Random.Range(0, available.Count)]);

    }

    public void setColor(ColorName color){
        this.required = new SortedDictionary<ColorName, bool>();
        List<ColorName> required = Palette.getRequiredForColor(color);

        foreach(ColorName subColor in required){
            this.required.Add(subColor, false);
        }

        this.outerColor = color;
        this.innerColor = ColorName.White;
        GetComponent<MeshRenderer>().material.color = Palette.getColorValue(color);
        setInnerColor(Palette.getWhite());

    }

    public ColorName getColor(){
        return outerColor;
    }

    void checkPaint(ColorName paintColor){

        if(innerColor != ColorName.White){
            Debug.Log("MIXED: " + paintColor.ToString() + " " + innerColor.ToString());
            ColorName colorName = Palette.getSimpleMix(new List<ColorName>{paintColor, innerColor});
            //ColorName colorName = Palette.getColorName(newColor);
            //print(Palette.getColorName(newColor));
            if(colorName != ColorName.White){
                paintColor = colorName;
            }
        }

        if(required.ContainsKey(paintColor) || paintColor == outerColor){
            changeColor(paintColor);
            checkNeighbors(paintColor);
        } 
        else{
            Debug.Log("Not Required: " + paintColor);
            // List<ColorName> keys = new List<ColorName>(required.Keys);
            // foreach(ColorName requiredColor in keys)
            // {
            //     required[requiredColor] = false;
            // }
            // setInnerColor(Palette.getWhite());
            // innerColor = ColorName.White;
        }

    }

    
    IEnumerator WaitToDie() {
        hit = true;
        yield return new WaitForSeconds(1f);
        //transform.position = respawn.transform.position.y - 0.5f;
        float newY = getNewY();
        print("RESPAWN: " + newY);

        print((newY - transform.position.y) > 20);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        neighbors = new List<Block>();
        //initialize(level.getAvailable());
        level.addBreak(getColor());
    }

    float getNewY(){
        return respawn.transform.position.y - 0.5f;

       //return transform.position.y + level.getRespawnOffset();
    }



    IEnumerator WaitForNeighbor(float time){
        yield return new WaitForSeconds(time);
        print("GETTING NEIGHBORS: " + getColor());
        getNeighbors();
    }

    void changeColor(ColorName addedColor){

        if(addedColor == outerColor){
            StartCoroutine(WaitToDie());
        }
        required[addedColor] = true;
        innerColor = addedColor;
        setInnerColor(Palette.getColorValue(addedColor));

    }

    public void checkNeighbors(ColorName newColor){
         //Check neighbors
        for(int i = 0; i < neighbors.Count; i++){
            if(neighbors[i] != null){
                bool alreadyChecked = neighbors[i].innerColor == this.innerColor;
                if(neighbors[i].getColor() == this.getColor() && !alreadyChecked){
                    //neighbors[i].checkPaint(newColor);
                    neighbors[i].changeColor(newColor);
                    neighbors[i].checkNeighbors(newColor);
                    // neighbors[i].checkPaint(addedColor);
                }
            }
        }
    }

    void setInnerColor(Color color){

        //Change the inner color
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        var temp = renderer.materials; 
        temp[1].color = color;
        renderer.materials = temp;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Paint"){
            Paint paint = collision.gameObject.GetComponent<Paint>();
            if(!hit){
                checkPaint(paint.getColor());
            }

        }
    }
    void setNeighbor(Block block, int direction){
        neighbors[direction] = block;
    }
}
