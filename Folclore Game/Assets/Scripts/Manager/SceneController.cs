using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    
    [SerializeField] private GameObject Player;
    [SerializeField] private float timeToFade;
    private CanvasGroup fade;
    
    private void Awake()
    {
        Instance = this;
        fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
    }

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
        fade.DOFade(1, timeToFade);
        SceneManager.LoadScene("CucaLevel");
        Player.transform.position = new Vector3(-85,-2,0);
        fade.DOFade(0, timeToFade);
    }

    public void CucaEsgoto()
    {
        SceneManager.LoadScene("CucaEsgoto");
    }

    public void CucaCasaAlquimista()
    {
        StartCoroutine(CasaAlquimista());
    }
    IEnumerator CasaAlquimista()
    {
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        SceneManager.LoadScene("CucaCasaAlquimista");
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
    }

    public void CucaCaminho()
    {
        StartCoroutine(Caminho());
    }
    IEnumerator Caminho()
    {
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        SceneManager.LoadScene("CucaLevel");
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
    }
    
}
