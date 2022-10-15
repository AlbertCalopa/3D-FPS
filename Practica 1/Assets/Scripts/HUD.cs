using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{

    float m_PlayerHealth;
    public GameObject textHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerHealth = GameController.GetGameController().GetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        textHealth.GetComponent<TextMeshProUGUI>().text = "Health: " + m_PlayerHealth;
    }
}
