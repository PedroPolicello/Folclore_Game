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
        player.transform.position = new Vector3(-85,-2,0);
        fade.DOFade(0, timeToFade);
    }

    void SetMapsToFalse()
    {
        for (int i = 0; i < activesScenes.Length; i++)
        {
            activesScenes[i].SetActive(false);
        }
    }

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
        //SceneManager.LoadScene("CucaCasaAlquimista");
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }

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
        //SceneManager.LoadScene("CucaLevel");
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }
    
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
        //SceneManager.LoadScene("CucaEsgoto");
        yield return new WaitForSeconds(.5f);
        fade.DOFade(0, timeToFade);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }
}
