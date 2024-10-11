using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPuzzle : MonoBehaviour
{
    public bool isFixed = false;

    public Transform tempRotationObject;
    public Transform puzzle;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    //Vector3 refV3;
    public float dragPower = 1;
    public float rotationFollowSpeed = 1;

    public bool isOnlyRotationX = false;
    public bool isOnlyRotationY = false;

    float tempCount = 20;

    public float rotatePower = .5f;

    public float angle;
    public float puzzleRotX;


    private void Awake()
    {
        float tempRotX = Random.Range(-180, 180);
        float tempRotY = Random.Range(-180, 180);
        float tempRotZ = Random.Range(-180, 180);
        tempRotationObject.rotation = Quaternion.Euler(tempRotX, tempRotY, tempRotZ);
        puzzle.rotation = Quaternion.Euler(tempRotX, tempRotY, tempRotZ);
    }
    // Update is called once per frame
    void Update()
    {



        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            this.transform.localRotation = Quaternion.identity;
            tempRotationObject.SetParent(this.transform);
        }

        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical") || Input.GetKeyUp(KeyCode.Keypad1) || Input.GetKeyUp(KeyCode.Keypad2))
        {
            tempRotationObject.parent = null;
        }

        if (Input.GetButton("Horizontal"))
        {
            this.transform.Rotate(0, Input.GetAxis("Horizontal") * rotatePower, 0, Space.World);
        }

        if (Input.GetButton("Vertical"))
        {
            this.transform.Rotate(Input.GetAxis("Vertical") * rotatePower, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
            this.transform.Rotate(0, 0, rotatePower, Space.World);
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            this.transform.Rotate(0, 0, -rotatePower, Space.World);
        }


        puzzle.rotation = Quaternion.Slerp(puzzle.rotation, tempRotationObject.rotation, rotationFollowSpeed);

        CheckAngle();

        if(isFixed)
        {
            tempRotationObject.rotation = Quaternion.Euler(90, 0, 0);
        }
    }


    void DragToRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.transform.localRotation = Quaternion.identity;
            tempRotationObject.SetParent(this.transform);
        }

        if (Input.GetMouseButtonUp(0))
        {
            tempRotationObject.parent = null;
        }

        if (Input.GetMouseButton(0))
        {
            mPosDelta = Input.mousePosition - mPrevPos;

            if (!isOnlyRotationX && !isOnlyRotationY)
            {
                if (mPosDelta.y < -tempCount || mPosDelta.y > tempCount)
                {
                    isOnlyRotationX = true;
                }
                else if (mPosDelta.x < -tempCount || mPosDelta.x > tempCount)
                {
                    isOnlyRotationY = true;
                }
            }



            if (isOnlyRotationY)
            {
                if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                    transform.Rotate(transform.up, -Vector3.Dot(mPosDelta * dragPower, Camera.main.transform.right), Space.World);
                else
                    transform.Rotate(transform.up, Vector3.Dot(mPosDelta * dragPower, Camera.main.transform.right), Space.World);
            }

            if (isOnlyRotationX)
            {
                transform.Rotate(Camera.main.transform.right, -Vector3.Dot(mPosDelta * dragPower, Camera.main.transform.up), Space.World);

            }

        }
        else
        {
            isOnlyRotationX = false;
            isOnlyRotationY = false;
            mPrevPos = Input.mousePosition;
        }

        if (isOnlyRotationX || isOnlyRotationY)
        {
            mPrevPos = Input.mousePosition;

        }
    }

    void CheckAngle()
    {
        Vector3 directionToLookAtTarget = Camera.main.transform.position - puzzle.position;
        angle = Quaternion.Angle(Quaternion.Euler(-90,0,0), puzzle.rotation);
        puzzleRotX = puzzle.eulerAngles.x;

        if (angle >= 178 && angle <= 182)
        {
            if(puzzleRotX >= 88 & puzzleRotX <= 92)
            {
                //isFixed = true;
                print("fixed!");
            }
        }
            

    }
}
