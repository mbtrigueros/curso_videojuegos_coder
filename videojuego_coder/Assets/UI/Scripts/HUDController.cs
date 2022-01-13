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
        PlayerController.onPlayerStarsChange += OnPlayerStarsChangeHandler;
        PlayerController.onPlayerDeath += OnPlayerDeathHandler;
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


    public void OnPlayerDeathHandler()
    {
        SceneManager.LoadScene("GameOver");
    }


    void OnPlayerLivesChangeHandler(int lives)
    {

        livesImg.sprite = livesSprites[lives];

    }
    void OnPlayerStarsChangeHandler(int stars)
    {
       if(animStar != null) animStar.SetTrigger("NewStar");
        textStars.text = stars.ToString();
    }


    
}
