using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private UnityEvent inTutorial;
    [SerializeField] private UnityEvent outTutorial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player")) inTutorial?.Invoke();
        
        
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            outTutorial?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
