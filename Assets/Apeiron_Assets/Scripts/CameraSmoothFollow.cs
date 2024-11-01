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

    public bool isFollowRotation;
    public float rotationSpeed = 1;

    public Animator camShakeAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerTarget != null)
        {
            cameraFocusPos = targetAvatar.position + (playerTarget.position- targetAvatar.position) * force;
        }else
        {
            cameraFocusPos = targetAvatar.position;
        }

        if(isFollowRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAvatar.rotation, rotationSpeed * Time.deltaTime);
        }

        transform.position = Vector3.SmoothDamp(transform.position, cameraFocusPos, ref refV3, speed);
    }
}
