using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] int selectLevel;
    int levelSelected;
    // Start is called before the first frame update
    void Start()
    {
        levelSelected = selectLevel;
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

        switch (levelSelected)
        {
            case 3:
                LevelManager.instance.ThirdLevel();
                break;
            case 2:
                LevelManager.instance.SecondLevel();
                break;
            case 1:
                LevelManager.instance.FirstLevel();
                break;
            default:
                LevelManager.instance.Landing();
                break;
        }
    }
}
