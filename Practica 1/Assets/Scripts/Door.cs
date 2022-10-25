using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public Key key;
    public GameObject text;
    float timerText = 2.5f;
    bool timerActivate = false;
    bool unlocked = false;
    
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
        float fadeSpeed = 0.4f;
        Color objectColor = this.GetComponent<MeshRenderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<MeshRenderer>().material.color = objectColor;
        //this.gameObject.SetActive(false);
        if (fadeAmount <= 0)
        {
            this.gameObject.SetActive(false);
            
        }
        
        //Instantiate(LifeItemPrefab, this.transform.position, this.transform.rotation);



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
                    text.GetComponent<TextMeshProUGUI>().text = "Muy bien! Ahora puedes ir a ver mi verdadero yo";
                    unlocked = true;
                    //this.gameObject.SetActive(false);
                }
                

            }
            else
            {
                
                if (timerText > 0)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Alto! Necesito la llave m�s ro�osa que puedas encontrar";
                    
                }
                timerActivate = true;
            }
        }
    }


}
