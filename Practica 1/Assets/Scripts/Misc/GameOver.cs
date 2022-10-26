using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    
    private void Start()
    {
        //GameController.DestroySingleton();
        Cursor.lockState = CursorLockMode.None;        
    }
    public void Restart()
    {
        SceneManager.LoadScene("Level 1");        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
