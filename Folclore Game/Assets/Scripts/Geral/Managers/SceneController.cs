using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    
    [SerializeField] private GameObject player;
    [SerializeField] private float timeToFade;
    private CanvasGroup fade;

    [SerializeField] private GameObject[] changeScenePos; 
    [SerializeField] private GameObject[] activesScenes;

    private void Awake()
    {
        Instance = this;
        fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetMapsToFalse();
    }

    private void Start()
    {
        StartCoroutine(StartGame(false));
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
    
    void SetMapsToFalse()
    {
        for (int i = 0; i < activesScenes.Length; i++)
        {
            activesScenes[i].SetActive(false);
        }
    }

    //START GAME
    public void Game()
    {
        StartCoroutine(StartGame(true));
    }
    IEnumerator StartGame(bool restart)
    {
        if (restart)
        {
            Time.timeScale = 1;
            fade.DOFade(1, timeToFade);
            yield return new WaitForSeconds(timeToFade + .5f);
            player.transform.position = new Vector3(-85,-2,0);
            yield return new WaitForSeconds(timeToFade + .5f);
            fade.DOFade(0, timeToFade);
            yield return new WaitForSeconds(timeToFade + .5f);
        }
        else
        {
            PlayerMovement.Instance.SetPlayerStatic(true);
            PlayerAttack.instance.SetCanAttack(false);
            fade.DOFade(0, timeToFade);
            yield return new WaitForSeconds(timeToFade + .5f);
            PlayerMovement.Instance.SetPlayerStatic(false);
            PlayerAttack.instance.SetCanAttack(true);
        }
    }

    //ALQUIMISTA
    public void CucaCasaAlquimista()
    {
        StartCoroutine(CasaAlquimista());
    }
    IEnumerator CasaAlquimista()
    {
        PlayerMovement.Instance.SetPlayerStatic(true);
        PlayerAttack.instance.SetCanAttack(false);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        activesScenes[0].SetActive(true); //Alquimista e Seita
        player.transform.position = changeScenePos[0].transform.position; //ToAlquimista
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        yield return new WaitForSeconds(.5f);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }
    
    //CAMINHO
    public void CucaCaminho(bool isEsgoto)
    {
        StartCoroutine(Caminho(isEsgoto));
    }
    IEnumerator Caminho(bool isEsgoto)
    {
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        SetMapsToFalse();//Alquimista, Seita e Esgoto
        if(isEsgoto) player.transform.position = changeScenePos[1].transform.position; //BackToCaminho(Esgoto)
        else player.transform.position = changeScenePos[3].transform.position; //BackToCaminho(Alquimista)
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        yield return new WaitForSeconds(.5f);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }
    
    //ESGOTO
    public void CucaEsgoto()
    {
        StartCoroutine(Esgoto());
    }
    IEnumerator Esgoto()
    {
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        activesScenes[1].SetActive(true);
        player.transform.position = changeScenePos[2].transform.position; //ToEsgoto
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        yield return new WaitForSeconds(.5f);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }

    //BOSS
    public void CucaBoss()
    {
        StartCoroutine(Boss());
    }
    IEnumerator Boss()
    {
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        activesScenes[2].SetActive(true);
        player.transform.position = changeScenePos[4].transform.position; //ToBoss
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        BossDialog.Instance.StartBossDialog();
    }

    //WIN SCREEN
    public void CallWinScreen()
    {
        StartCoroutine(WinScreen());
    }
    IEnumerator WinScreen()
    {
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        activesScenes[3].SetActive(true);
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
    }
}
