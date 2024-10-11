using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireAutoRotate : MonoBehaviour
{
    public Vector3 RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime);
    }
}
