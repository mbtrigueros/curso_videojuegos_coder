using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originPosition = transform.localPosition;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, originPosition.z), Time.deltaTime);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originPosition;
    }
}
