using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool dontDestroyOnLoad;


    private string lvl1 = "Level_01";
    private string lvl2 = "Level_02";
    private string lvl3 = "Level_03";
    private string gameOver = "GameOver";
    private string landing = "Landing";
    private string youWin = "YouWin";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        PlayerController.onPlayerDeath += GameOver;

    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOver);
    }
    public int GetLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void Landing()
    {
        SceneManager.LoadScene(landing);
    }
    public void FirstLevel()
    {
        SceneManager.LoadScene(lvl1);
    }

    public void SecondLevel()
    {
        SceneManager.LoadScene(lvl2);
    }

    public void ThirdLevel()
    {
        SceneManager.LoadScene(lvl3);
    }

    public void YouWin()
    {
        SceneManager.LoadScene(youWin);
    }
}
