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
        if (LevelManager.instance.GetLevel() >= 1)
        {
            AudioManager.instance.PlaySound("SoundTrack_02");
            AudioManager.instance.StopSound("SoundTrack_01");
        }

        switch (level)
        {
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
