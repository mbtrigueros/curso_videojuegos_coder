using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    Animator animMirror;

    // Start is called before the first frame update
    void Start()
    {
        animMirror = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotation() {

        animMirror.SetTrigger("Rotation");
        StartCoroutine(DeactivateCollider());
    }

    public IEnumerator DeactivateCollider()
    {
        float elapsed = 0f;

        while (elapsed < 2f)
        {
            GetComponent<Collider>().enabled = false;
            elapsed += Time.deltaTime;
            yield return null;

        }

        GetComponent<Collider>().enabled = true;
    }
}
