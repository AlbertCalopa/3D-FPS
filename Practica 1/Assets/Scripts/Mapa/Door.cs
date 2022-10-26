using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public Key key;
    public WrongKey wrongKey;
    public GameObject text;
    float timerText = 2.5f;
    bool timerActivate = false;
    bool unlocked = false;
    public ParticleSystem particulas;
    
    // Start is called before the first frame update
    void Start()
    {
        timerText = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActivate)
        {            
            timerText -= Time.deltaTime;
            Debug.Log(timerText);
            if (timerText < 0)
            {
                timerText = 2.5f;
                text.GetComponent<TextMeshProUGUI>().text = "";
                
                timerActivate = false;
            }
            
        }
        if (unlocked)
        {
            Fade();            
        }

        Debug.Log(key.hasKey);
    }
    void Fade()
    {
        particulas.Play();
        float fadeSpeed = 0.4f;
        Color objectColor = this.GetComponent<MeshRenderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<MeshRenderer>().material.color = objectColor;
        
        if (fadeAmount <= 0)
        {
            this.gameObject.SetActive(false);
            
        }                  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (key.hasKey)
            {
                timerActivate = true;
                if (timerText > 0)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Muy bien! Ahora puedes acceder a mi corazón.";
                    unlocked = true;
                    //this.gameObject.SetActive(false);
                }
                

            }           

            else if (wrongKey.hasWrongKey)
            {
                timerActivate = true;
                if (timerText > 0)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Está no es la llave que había pedido.";                
                }
            }

            else
            {

                if (timerText > 0)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Alto! Necesito la llave más roñosa que puedas encontrar.";

                }
                timerActivate = true;
            }
        }
    }


}
