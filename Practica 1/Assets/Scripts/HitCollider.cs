using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public float m_Life;

    public DronEnemy2 m_DronEnemy;

    public void Hit()
    {
        m_DronEnemy.Hit(m_Life);
    }
}
