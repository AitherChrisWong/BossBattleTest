using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarDashForceRecallApostle : MonoBehaviour
{
    public AvatarBasicMovement avatarBasicMovement;
    bool isCallbackApostle;

    public Transform[] apostles;
    public Transform[] callbackPos;

    public GameObject vfxSpawnApostle;

    public Vector3 offsetPos;
    public float tempDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (avatarBasicMovement.isDash)
        {
            isCallbackApostle = true;
        }*/

        if (Input.GetKeyDown("space"))
        {
            if(avatarBasicMovement.isDash)
            {
                offsetPos = avatarBasicMovement.dirCube.transform.localPosition * tempDistance;

                if (avatarBasicMovement.dashCurrentProgess >= 50)
                {
                    for (int i = 0; i < apostles.Length; i++)
                    {
                        Vector3 targetPos = callbackPos[i].position + offsetPos;

                        apostles[i].position = targetPos;
                        GameObject vfx = Instantiate(vfxSpawnApostle);
                        vfx.transform.position = targetPos;
                        Destroy(vfx, 2);
                    }
                }
            }

            
        }
            

        //isCallbackApostle = false;
    }
}
