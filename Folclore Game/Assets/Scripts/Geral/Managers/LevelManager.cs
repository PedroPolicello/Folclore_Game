using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fade;

    public void Cuca()
    {
        StartCoroutine(LoadCucaLevel());
    }

    IEnumerator LoadCucaLevel()
    {
        fade.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CucaLevel");
    }
}
