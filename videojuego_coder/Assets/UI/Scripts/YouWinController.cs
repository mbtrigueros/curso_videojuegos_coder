using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinController : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnClickPlay()
    {
        LevelManager.instance.YouWin();
        

    }
}
