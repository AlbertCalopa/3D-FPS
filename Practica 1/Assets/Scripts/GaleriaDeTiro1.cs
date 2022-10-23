using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaleriaDeTiro1 : MonoBehaviour
{
    public float TimerGallery = 5.0f;

    MeshCollider mesh;

    Renderer render;

    public FPPlayerController fppController;



    // Start is called before the first frame update
    void Start()
    {
        mesh = this.gameObject.GetComponent<MeshCollider>();
        render = this.gameObject.GetComponent<Renderer>();
        
        
    }

    // Update is called once per frame


    void Update()
    {

        
        

        TimerGallery -= Time.deltaTime;

        if (TimerGallery <= 0)
        {
            mesh.enabled = true;
            render.enabled = true;
        }
    }

    public void Timer()
    {        
        mesh.enabled = false;
        render.enabled = false;
        TimerGallery = 3.0f;

    }
}
