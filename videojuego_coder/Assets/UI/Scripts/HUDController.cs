using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textStars;
    [SerializeField] private TextMeshProUGUI textLives;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStars();
        UpdateLives();
    }

    void UpdateStars()
    {
        int stars = GameManager.instance.GetStars();

        textStars.text = "" + stars;

    }

    void UpdateLives()
    {

        int lives = GameManager.instance.GetPlayerLives();

        textLives.text = "" + lives;
    }
}
