using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRandomScale : MonoBehaviour
{
    public Vector2 minScale;
    public Vector2 maxScale;

    Vector3 refV3;

    public float tempSmooth = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float randomX = Random.Range(minScale.x, maxScale.x);
        float randomY = Random.Range(minScale.y, maxScale.y);
        Vector3 targetScale = new Vector3(randomX, randomY,1);

        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref refV3, tempSmooth);
    }
}
