using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchCameras : MonoBehaviour
{

    [SerializeField] private UnityEvent onSwitchCameras;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
          if (other.gameObject.CompareTag("Player"))
        {
            onSwitchCameras?.Invoke();

        }
    }
}