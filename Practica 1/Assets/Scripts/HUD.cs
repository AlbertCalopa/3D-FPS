using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    
    public GameObject textHealth;
    public FPPlayerController fppController;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        textHealth.GetComponent<TextMeshProUGUI>().text = "Health: " + fppController.m_Health;
    }
}
