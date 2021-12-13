using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textStars;
    [SerializeField] private TextMeshProUGUI textLives;

    private void Awake()
    {
        PlayerController.onPlayerLivesChange += OnPlayerLivesChangeHandler;
        PlayerController.onPlayerStarsChange += OnPlayerStarsChangeHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.onPlayerDeath += OnPlayerDeathHandler;
    }

    // Update is called once per frame
    void Update()
    {
       // UpdateStars();
    }

    //void UpdateStars()
    //{
    //    int stars = GameManager.instance.GetStars();

    //    textStars.text = stars.ToString();

    //}

    void OnPlayerLivesChangeHandler(int lives)
    {

        textLives.text = lives.ToString();
    }
    void OnPlayerStarsChangeHandler(int stars)
    {

        textStars.text = stars.ToString();
    }

    void OnPlayerDeathHandler()
    {
        SceneManager.LoadScene("GameOver");
    }
}
