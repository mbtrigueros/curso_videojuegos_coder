using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public bool dontDestroyOnLoad;
    public static int playerStars = 0; //cantidad de stars del jugador
    public static int playerLives = 5; //cantidad de vidas del jugador

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.onPlayerDeath += GameOver;
    }

    public int GetStars()
    {
        return playerStars;
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        ResetLives();
        ResetStars();
        ResetGravity();


    }

    public void LevelChange()
    {
        ResetLives();
        ResetGravity();


    }

    public int GetPlayerLives()
    {
        return playerLives;
    }

    public void ResetStars()
    {
        playerStars = 0;
    }

    public void ResetLives()
    {
        playerLives = 5;
    }

    public void ResetGravity()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetGravity(new Vector3(0f, -9.8f, 0f));
    }
    


}
