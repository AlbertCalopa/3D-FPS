using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameController.GetGameController().SetPlayerHealth(0.3f);
            SceneManager.LoadSceneAsync("Level 2");
        }
    }
}
