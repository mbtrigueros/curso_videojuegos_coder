using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{

    [SerializeField] private Text textStars;

    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private Image livesImg;

    private Animator animStar;

    private void Awake()
    {
        PlayerController.onPlayerDeath += OnPlayerDeathHandler;
        PlayerController.onPlayerStarsChange += OnPlayerStarsChangeHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.onPlayerLivesChange += OnPlayerLivesChangeHandler;
        animStar = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
        PlayerController.onPlayerDeath -= OnPlayerDeathHandler;
        PlayerController.onPlayerLivesChange -= OnPlayerLivesChangeHandler;
        PlayerController.onPlayerStarsChange -= OnPlayerStarsChangeHandler;
    }


    void OnPlayerLivesChangeHandler(int lives)
    {

        livesImg.sprite = livesSprites[lives];

    }
    void OnPlayerStarsChangeHandler(int stars)
    {
        animStar.SetTrigger("NewStar");
        textStars.text = stars.ToString();
    }

    void OnPlayerDeathHandler()
    {
        SceneManager.LoadScene("GameOver");
    }
}
