using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class StarsManager : MonoBehaviour
{
    public UnityEvent onComplete; //metodo onComplete para desactivar los obstaculos una vez recogidas todas las stars en escena 
    private GameObject[] stars;


    private void Update()
    {

        stars = GameObject.FindGameObjectsWithTag("Star"); //busco los gameObject con el Tag Star
        GetSceneStars();

        if (GetSceneStars() == 0) //si el largo del array es igual a 0, invoco al unity event onComplete
        {
            onComplete?.Invoke();
        }
    }

    public int GetSceneStars() //metodo para conseguir el largo del array de gameObjects con el Tag Star
    {
        Debug.Log("Cantidad de stars en escena: " + stars.Length);
        return stars.Length;
    }    

}
