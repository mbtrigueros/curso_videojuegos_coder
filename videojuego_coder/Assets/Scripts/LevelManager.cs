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
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LevelChange(int numero)
    {
        StartCoroutine(LevelLoader(numero));
    }

    IEnumerator LevelLoader(int numero)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + numero);
        
        while (!operation.isDone)
        {
            Debug.Log("Progreso de carga " + operation.progress);
            yield return null;
        }
    }
}
