using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform targetFollower;
    public Transform playerTarget;
    Vector3 cameraFocusPos;
    public float force = 0;

    Vector3 refV3;
    public float speed = .1f;
    

    [Header("Cast Cam offset")]
    public bool isCastingCamOffset;
    public float camZoomLevel = 25;
    public float offsetPower = .01f;
    public float offsetDamping = .1f;
    

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
            cameraFocusPos = targetFollower.position + (playerTarget.position- targetFollower.position) * force;
        }else
        {
            cameraFocusPos = targetFollower.position;
        }

        if(isFollowRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetFollower.rotation, rotationSpeed * Time.deltaTime);
        }

        transform.position = Vector3.SmoothDamp(transform.position, cameraFocusPos, ref refV3, speed);
    }

    private void Update()
    {
        Vector3 targetOffset = Vector3.zero;

        if(isCastingCamOffset)
        {
            Vector3 curMousePos = Input.mousePosition;
            curMousePos.x -= Screen.width / 2;
            curMousePos.y -= Screen.height / 2;

            targetOffset = new Vector3(curMousePos.x * offsetPower, 0, curMousePos.y * offsetPower);
            //transform.GetChild(0).localPosition = new Vector3(curMousePos.x * offsetPower, 0, curMousePos.y * offsetPower);
        }
        else
        {
            targetOffset = Vector3.zero;
        }

        transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, targetOffset, offsetDamping);


        
    }

}
