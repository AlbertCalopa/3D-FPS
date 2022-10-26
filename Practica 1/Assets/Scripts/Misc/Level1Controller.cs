using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : MonoBehaviour
{
    GameController Controller;

    public bool changeScene = false;
    private void Start()
    {
        Controller = GameController.GetGameController();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameController.GetGameController().SetPlayerHealth(Controller.GetPlayerHealth());
            GameController.GetGameController().SetPlayerShield(Controller.GetPlayerShield());
            SceneManager.LoadScene("Level 2");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.GetGameController().SetPlayerHealth(Controller.GetPlayerHealth());
            GameController.GetGameController().SetPlayerShield(Controller.GetPlayerShield());
            GameController.GetGameController().SetMaxBullets(Controller.GetMaxBullets());
            changeScene = true;
            SceneManager.LoadScene("Level 2");
            
        }
    }
}
