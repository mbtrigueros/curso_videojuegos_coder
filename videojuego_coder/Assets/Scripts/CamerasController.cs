using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private Transform target; //establezco donde va a mirar la main camara
    private Vector3 offset;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        offset = (transform.position - target.position); //el offset va a ser la distancia entre la posicion de la camara y la posicion del target
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() //es recomendable que los cambios en la camara se hagan en el LateUpdate para mejor performance
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 2); //interpolo entre la posicion de la camara y del target

    }

    public void ResetRotation()
    {
        transform.rotation = originalRotation;
    }

    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, -180);
    }

}
