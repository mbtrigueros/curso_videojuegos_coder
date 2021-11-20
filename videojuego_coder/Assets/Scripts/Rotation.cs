using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    [SerializeField] private Vector3 rotateVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------

    private void Rotate() //rotacion en el eje establecido a lo largo del tiempo
    {
        transform.Rotate(rotateVector * Time.deltaTime);
    }
}
