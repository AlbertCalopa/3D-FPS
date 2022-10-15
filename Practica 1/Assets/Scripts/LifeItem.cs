using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeItem : Item
{
    public float m_Life;
    public override void Pick(FPPlayerController Player)
    {
        if (Player.getLife() < 1.0f)
        {
            Player.AddLife(m_Life);
            Debug.Log(m_Life);
            gameObject.SetActive(false);
        }
    }
}
