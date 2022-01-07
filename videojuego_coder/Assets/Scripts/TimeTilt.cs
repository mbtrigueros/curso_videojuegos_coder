using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTilt : MonoBehaviour
{
    [SerializeField] float timeScale = 0.2f;
    [SerializeField] float duration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TimeChange()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Time.timeScale = timeScale;
            elapsed += Time.deltaTime;
            yield return null;

        }

        Time.timeScale = 1f;
    }
}
