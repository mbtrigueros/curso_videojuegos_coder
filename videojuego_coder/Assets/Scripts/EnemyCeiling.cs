using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCeiling : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        Rotation(); //giro al enemigo del techo 
    }

    private void Rotation()
    {
        transform.Rotate(new Vector3(0, 0, -180), Space.World);
    }
}
