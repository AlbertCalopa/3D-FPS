using UnityEngine;
public class GameController : MonoBehaviour
{
    static GameController m_GameController = null;
    float m_PlayerHealth = 1.0f;
    float m_PlayerShield = 1.0f;
    FPPlayerController m_Player;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public static GameController GetGameController()
    {
        if (m_GameController == null)
        {
            m_GameController = new GameObject("GameController").AddComponent<GameController>();
            GameControllerData l_GameControllerData = Resources.Load<GameControllerData>("Data");
            m_GameController.m_PlayerHealth = l_GameControllerData.m_Lifes;
            m_GameController.m_PlayerShield = l_GameControllerData.m_Shield;
            Debug.Log("Data loaded with file" + m_GameController.m_PlayerHealth);
        }
        return m_GameController;
    }
    public static void DestroySingleton()
    {
        if (m_GameController != null)
        {
            GameObject.Destroy(m_GameController);            
        }
        m_GameController = null;
    }
    public void SetPlayerHealth(float PlayerHealth)
    {
        m_PlayerHealth = PlayerHealth;
    }
    public float GetPlayerHealth()
    {
        return m_PlayerHealth;
    }
    public void SetPlayerShield(float PlayerShield)
    {
        m_PlayerShield = PlayerShield;
    }
    public float GetPlayerShield()
    {
        return m_PlayerShield;
    }
    public FPPlayerController GetPlayer()
    {
        return m_Player;
    }    
    public void SetPlayer(FPPlayerController Player)
    {
        m_Player = Player;
    }
}
