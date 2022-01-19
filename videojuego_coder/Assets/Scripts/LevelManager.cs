using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool dontDestroyOnLoad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
       
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GetLevel() <= 1)
        {
            AudioManager.instance.PlaySound("SoundTrack_01");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void LevelChange(int numero)
    {
        StartCoroutine(LevelLoader(numero));
    }

    public int GetLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator LevelLoader(int numero)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GetLevel() + numero);
        
        while (!operation.isDone)
        {
            Debug.Log("Progreso de carga " + operation.progress);
            yield return null;
        }
    }
}
