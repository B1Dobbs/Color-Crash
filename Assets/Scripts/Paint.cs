using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Palette;

public class Paint : MonoBehaviour
{
    ColorName color;

    ParticleSystem explosion;
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void setColor(ColorName color){
        this.color = color;

        GetComponent<MeshRenderer>().material.color = Palette.getColorValue(color);
        GetComponent<ParticleSystemRenderer>().material.color = Palette.getColorValue(color);
    }

    public ColorName getColor(){
        return color;
    }

    void OnCollisionEnter(Collision collision)
    {
//        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Block"){
            explosion.Play();
            //mesh.enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(WaitToDie());

        }
    }

     IEnumerator WaitToDie() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
