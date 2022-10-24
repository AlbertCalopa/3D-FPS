using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : Item
{
    public float maxBullets;
    // Start is called before the first frame update
    public override void Pick(FPPlayerController Player)
    {
        if (Player.getBullets() < 100.0f)
        {
            Player.AddBullets(maxBullets);            
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
