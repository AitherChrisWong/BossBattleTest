using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotatePVP : MonoBehaviour
{
    public Vector3 rotateSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.localRotation *= Quaternion.Euler(rotateSpeed.x, rotateSpeed.y, rotateSpeed.z);
    }
}
