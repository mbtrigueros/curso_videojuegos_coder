using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        if (LevelManager.instance.GetLevel() >= 1)
        {
            AudioManager.instance.PlaySound("SoundTrack_02");
            AudioManager.instance.StopSound("SoundTrack_01");
        }

        LevelManager.instance.LevelChange(1);
        GameManager.instance.LevelChange();
    }
}
