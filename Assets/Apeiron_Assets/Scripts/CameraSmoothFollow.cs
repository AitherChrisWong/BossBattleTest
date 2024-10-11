using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform targetAvatar;
    public Transform playerTarget;
    Vector3 cameraFocusPos;
    public float force = 0;

    Vector3 refV3;
    public float speed = .1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTarget != null)
        {
            cameraFocusPos = targetAvatar.position + (playerTarget.position- targetAvatar.position) * force;
        }else
        {
            cameraFocusPos = targetAvatar.position;
        }

        transform.position = Vector3.SmoothDamp(transform.position, cameraFocusPos, ref refV3, speed);
    }
}
