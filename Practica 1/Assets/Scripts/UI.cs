using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public Canvas canvas;
    public TMP_Text text;
    float points = 0;
    public GameObject cubo;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Points: " + points;
    }

    // Update is called once per frame
    void Update()
    {
        
        //points++;
       
        text.text = "Points: " + points;
        
    }

    void Punts()
    {
        if (!cubo.activeInHierarchy)
        {
            points += 10;
        }
    }
}
