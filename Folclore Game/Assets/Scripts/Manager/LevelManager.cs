using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private String[] level;

    public void Cuca()
    {
        SceneManager.LoadScene(level[0]);
    }
}
