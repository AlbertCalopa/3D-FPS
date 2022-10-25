using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public Key key;
    public GameObject text;
    float timerText = 3.0f;
    bool timerActivate = false;
    
    // Start is called before the first frame update
    void Start()
    {
       
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
                text.GetComponent<TextMeshProUGUI>().text = "";
                timerText = 3.0f;
                timerActivate = false;
            }
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
                    text.GetComponent<TextMeshProUGUI>().text = "Muy bien! Ahora puedes ir a ver mi verdadero yo";
                    this.gameObject.SetActive(false);
                }
                

            }
            else
            {
                
                if (timerText > 0)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Alto! Necesito la llave más roñosa que puedas encontrar";
                    
                }
                timerActivate = true;
            }
        }
    }


}
