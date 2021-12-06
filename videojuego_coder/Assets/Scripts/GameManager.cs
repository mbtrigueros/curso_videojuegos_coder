using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public bool dontDestroyOnLoad;
    public static int playerStars = 0; //cantidad de stars del jugador
    public static int playerLives = 100; //cantidad de vidas del jugador


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
        playerStars = 0;
        playerLives = 100;
    }

    public int GetPlayerLives()
    {
        return playerLives;
    }
}
