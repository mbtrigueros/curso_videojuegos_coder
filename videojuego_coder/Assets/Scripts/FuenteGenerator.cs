using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteGenerator : MonoBehaviour
{

    [SerializeField] private GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generator", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Generator()
    {
        Instantiate(obstacle, transform);

    }
}
