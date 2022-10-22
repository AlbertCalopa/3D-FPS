using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    
    public GameObject textHealth;
    public GameObject textShield;
    public GameObject textAmmo;
    public GameObject textPoints;
    public GameObject textGallery; 

    public FPPlayerController fppController;
    // Start is called before the first frame update
    void Start() 
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        textHealth.GetComponent<TextMeshProUGUI>().text = "HEALTH: " + fppController.m_Health * 100.0f; 
        textShield.GetComponent<TextMeshProUGUI>().text = "SHIELD: " + fppController.m_Shield * 100.0f;
        textAmmo.GetComponent<TextMeshProUGUI>().text = "AMMO: " + fppController.m_bullets + "/" + fppController.m_MaxBullets;

        if (fppController.isGaleryActive)
        {
            textGallery.SetActive(true);
            textPoints.GetComponent<TextMeshProUGUI>().text = "POINTS: " + fppController.m_Points;
        }

        if (fppController.pointsActive)
        {
            textGallery.SetActive(false);
            textPoints.SetActive(true);
            textPoints.GetComponent<TextMeshProUGUI>().text = "POINTS: " + fppController.m_Points;
        }
    }
}
