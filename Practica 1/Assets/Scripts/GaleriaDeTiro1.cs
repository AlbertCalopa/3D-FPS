using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaleriaDeTiro1 : MonoBehaviour
{
    

    MeshCollider mesh;

    Renderer render; 



    // Start is called before the first frame update
    void Start()
    {
        mesh = this.gameObject.GetComponent<MeshCollider>();
        render = this.gameObject.GetComponent<Renderer>();
        
        
    }

    // Update is called once per frame


    void Update()
    {
        
        
            //mesh.enabled = true;
            //render.enabled = true;
        



    }

    public void Timer()
    {  
        
        mesh.enabled = false;
        render.enabled = false;
        

    }
}
