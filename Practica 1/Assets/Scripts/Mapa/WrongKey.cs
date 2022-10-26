using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongKey : MonoBehaviour
{
    public bool hasWrongKey = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hasWrongKey = true;
            this.gameObject.SetActive(false);
        }
    }
}
