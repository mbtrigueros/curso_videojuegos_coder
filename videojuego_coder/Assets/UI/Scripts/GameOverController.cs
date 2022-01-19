using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class GameOverController : MonoBehaviour
{



    public void OnClickPlay()
    {
        LevelManager.instance.Landing();
        Debug.Log("Presione TRY AGAIN");

    }
}
