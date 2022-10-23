using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : MonoBehaviour
{
    GameController Controller;
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
}
