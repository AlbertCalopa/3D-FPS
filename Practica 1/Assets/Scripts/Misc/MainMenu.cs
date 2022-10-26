using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Level1Controller level;
    private void Start()
    {
        //GameController.DestroySingleton();
        Cursor.lockState = CursorLockMode.None;
        
        
    }
    public void Restart()
    {
        if (level.changeScene)
        {
            SceneManager.LoadScene("Level 2");
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
