using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class GameOverController : MonoBehaviour
{
    int lastLevel;

    // Start is called before the first frame update
    void Start()
    {
        lastLevel = LevelManager.instance.GetLevel() - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickPlay()
    {

        switch (lastLevel)
        {
            case 2:
                LevelManager.instance.LevelChange(-1);
                break;
            case 1:
                LevelManager.instance.LevelChange(-2);
                break;
            default:
                LevelManager.instance.LevelChange(-3);
                break;
        }

        Debug.Log("Presione TRY AGAIN");

    }
}
