using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGame()
    {
        //Cutscene...
        SceneManager.LoadScene("LevelSelector");
    }

    public void ExitGame()
    {
        Application.Quit();
        print("Saindo...");
    }
}
