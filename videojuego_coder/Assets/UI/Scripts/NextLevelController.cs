using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelController : MonoBehaviour
{

    private int level;

    // Start is called before the first frame update
    void Start()
    {
        level = LevelManager.instance.GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        switch (level)
        {
            case 4:
                LevelManager.instance.YouWin();
                break;
            case 3:
                LevelManager.instance.GameOver();
                break;
            case 2:
                LevelManager.instance.ThirdLevel();
                break;
            case 1:
                LevelManager.instance.SecondLevel();
                break;
            default:
                LevelManager.instance.FirstLevel();
                break;
        }
        GameManager.instance.LevelChange();
    }
}
