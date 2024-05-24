using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LevelSelector()
    {
        //Cutscene...
        SceneManager.LoadScene("LevelSelector");
    }

    public void ExitGame()
    {
        Application.Quit();
        print("Saindo...");
    }

    public void Game()
    {
        SceneManager.LoadScene("CucaLevel");
        Player.transform.position = new Vector3(0,-1.5f,0);
    }
    
}
