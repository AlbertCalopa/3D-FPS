using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    
    public GameObject textHealth;
    public GameObject textShield;
    public GameObject textAmmo;
    public GameObject textPoints;
    public GameObject textGallery1;

    public Button RestartButton;
    public Button ExitButton;
    public GameObject textGameOver;
    public Image imageDeathLvl2;

    string SceneName;

    public FPPlayerController fppController;
    // Start is called before the first frame update
    void Start() 
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneName = currentScene.name;
        

    }

    // Update is called once per frame
    void Update()
    {
        textHealth.GetComponent<TextMeshProUGUI>().text = "      : " + fppController.m_Health * 100.0f;
        textShield.GetComponent<TextMeshProUGUI>().text = "      : " + fppController.m_Shield * 100.0f;
        textAmmo.GetComponent<TextMeshProUGUI>().text = "         : " + fppController.m_bullets + "/" + fppController.m_MaxBullets;

        if (fppController.isGaleryActive)
        {
            textGallery1.SetActive(true);

        }
        else
        {
            textGallery1.SetActive(false);
        }

        if (fppController.pointsActive)
        {
            textGallery1.SetActive(false);

            textPoints.SetActive(true);
            textPoints.GetComponent<TextMeshProUGUI>().text = "      : " + fppController.m_Points + "Pts.";
        }
        else
        {
            textPoints.SetActive(false);
        }
    }

    public void ActivateHudGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        RestartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        textGameOver.SetActive(true);
        textShield.SetActive(false);
        textHealth.SetActive(false);
        textAmmo.SetActive(false);
        imageDeathLvl2.gameObject.SetActive(true);
        textGallery1.SetActive(false);
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneName);        
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
