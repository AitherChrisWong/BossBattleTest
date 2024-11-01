using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using System.Data.OleDb;

public class AvatarBasicMovement : MonoBehaviour
{
    public int currentHp;
    public int MaxHp = 100000;
    ApostleHp apostleHp;

    public CameraSmoothFollow cameraSmoothFollow;
    public bool isCastingSkill;
    public bool isAACharging;
    public bool isAAReady;
    public bool isAttacking;
    public bool isMoving;
    public bool isDash;

    public bool isMoveLocalTransform;
    public Transform tempLocalObject;
    public float moveSpeed;
    [HideInInspector]    public Vector3 moveDirection;
    public Transform dirCube;
    public DrawLineRenderer drawLineRenderer;

    [Header("Dash")]
    public GameObject VFXDash;
    public AnimationCurve dashCurve;
    public float dashTime;
    public float dashSpeed = 1f;
    public float dashDistance;
    Vector3 tempDashDir;

    public Image dashProgressBar;
    public GameObject dashProgressSlot1;
    public GameObject dashProgressSlot2;
    public float dashCurrentProgess;
    public float dashMaxProgress;
    public float dashProgressSpeed;


    public SkinnedMeshRenderer matSkin;
    public SkinnedMeshRenderer matOutlineSkin;
    public MeshRenderer matWeapon;
    public MeshRenderer matOutlineWeapon;

    public AnimationCurve dashSkinDissolve;
    float dashDissolveTime = 1;
    public float dashDissolveSpeed;

    Vector3 oldPos;


    [Header("Skin Setting")]
    public Transform skin;
    public Animator anim;
    string currentAnimState = null;

    public float rotSpeed = 10;


    [Header("Target System")]
    public SpriteRenderer targetRange;
    public Vector3 skillLookPos;
    public GameObject nearestTarget;
    public List<GameObject> targets = new List<GameObject>();

    [Header("Timeline")]
    public GameObject avatarAA;
    public PlayableAsset AACharge;
    public PlayableAsset AAStart;
    public GameObject AAProjectile;
    public GameObject VFXAAExplosion;
    public AudioClip SFXAAChargeEnd;

    [Header("Skill Status")]
    public PlayableDirector currentTimeline;
    public bool isSkillMovable;
    public bool isSkillDashable;
    public bool isSkillForceMove;
    public Vector3 skillMovePosition;
    public float skillMoveSpeed = 1;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        oldPos = transform.position;

        rb = GetComponent<Rigidbody>();
        //apostleHp = transform.Find("CanvasApostleHP").GetComponent<ApostleHp>();

        currentHp = MaxHp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        AvatarDashSystem();


        if (!isAttacking)
        {
            if (Input.GetKeyDown("space"))
            {
                if(dashCurrentProgess > 50)
                {
                    if(isCastingSkill)
                    {
                        if(isSkillDashable)
                        {
                            isCastingSkill = false; // force cancel casting skill when dash
                            AvatarDash();
                        }
                    }
                    else
                    {
                        AvatarDash();
                    }
                }
            }
        }
        

        if(dashDissolveTime < 1)
        {
            matSkin.material.SetFloat("_dissolve", dashSkinDissolve.Evaluate(dashDissolveTime));
            matWeapon.material.SetFloat("_dissolve", dashSkinDissolve.Evaluate(dashDissolveTime));
            matOutlineSkin.material.SetFloat("_dissolve", -dashSkinDissolve.Evaluate(dashDissolveTime));
            matOutlineWeapon.material.SetFloat("_dissolve", -dashSkinDissolve.Evaluate(dashDissolveTime));

            dashDissolveTime += Time.deltaTime / dashDissolveSpeed;
        }

        if (!isCastingSkill)
        {
            if (!isAttacking)
            {
                if (isDash)
                {
                    if (dashTime < 1)
                    {
                        transform.position += tempDashDir * dashCurve.Evaluate(dashTime) * dashDistance;
                        dashTime += Time.deltaTime / dashSpeed;
                    }
                    else
                    {
                        isDash = false;
                    }
                }
                else
                {
                    AvatarMove();
                }

                FindNearestTarget();
            }
        }
        else //is casting skill
        {
            if (isSkillForceMove)
            {
                transform.position = Vector3.Lerp(transform.position, skillMovePosition, skillMoveSpeed);
            }

            if(isSkillMovable)
            {
                AvatarMove();
            }
        }



        {   //adjust temp dir
            Vector3 tempDir = Vector3.zero;

            if (Input.GetKey("w"))
            {
                isMoving = true;
                tempDir.z += 1;
                //transform.position += new Vector3(0, 0, moveSpeed);
            }
            if (Input.GetKey("s"))
            {
                isMoving = true;
                tempDir.z -= 1;
                //transform.position += new Vector3(0, 0, -moveSpeed);
            }
            if (Input.GetKey("a"))
            {
                isMoving = true;
                tempDir.x -= 1;
                //transform.position += new Vector3(-moveSpeed, 0, 0);
            }
            if (Input.GetKey("d"))
            {
                isMoving = true;
                tempDir.x += 1;
                //transform.position += new Vector3(moveSpeed, 0, 0);
            }


            if (tempDir.x + tempDir.z < -1 && tempDir.x + tempDir.z > 1)
            {

            }
            else
            {
                tempDir.x *= .66f;
                tempDir.z *= .66f;
            }

            dirCube.localPosition = tempDir * 1;
        }


        /*moveDirection = transform.position - oldPos;
        dirCube.localPosition = moveDirection * 100;
        oldPos = transform.position;*/


        if (isCastingSkill) //lock rotation when casting skill
        {
            Vector3 lookDirection = skillLookPos - skin.transform.position;
            lookDirection.Normalize();

            skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, Quaternion.LookRotation(lookDirection), rotSpeed * Time.deltaTime);

        }
        else if(nearestTarget || isMoveLocalTransform)
        {
            Vector3 lookDirection = nearestTarget.transform.position - skin.transform.position;
            lookDirection.Normalize();

            skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, Quaternion.LookRotation(lookDirection), rotSpeed * Time.deltaTime);

        }
        else
        {
            if(isMoving)
            {
                Vector3 lookDirection = dirCube.position - skin.transform.position;
                lookDirection.Normalize();

                skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, Quaternion.LookRotation(lookDirection), rotSpeed * Time.deltaTime);
            }
            
        }

        if (isMoving) // update look target direction
        {
            UpdateAnim("move");
        }
        else if(isDash)
        {
            UpdateAnim("dash");
        }
        else
        {
            UpdateAnim("idle");
        }

        AvatarAA();
        isMoving = false;

        
    }

    void AvatarDash()
    {
        dashCurrentProgess -= 50;

        isMoving = false;
        isDash = true;
        dashTime = 0;
        dashDissolveTime = 0;
        tempDashDir = dirCube.localPosition;
        tempDashDir.Normalize();

        GameObject tempDash = Instantiate(VFXDash);
        tempDash.transform.position = skin.position;
        tempDash.transform.rotation = skin.rotation;
        Destroy(tempDash, 1);

        UpdateAnim("dash");
    }

    void AvatarMove()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey("w"))
        {
            isMoving = true;
            moveDir.z += 1;
            //transform.position += new Vector3(0, 0, moveSpeed);
        }
        if (Input.GetKey("s"))
        {
            isMoving = true;
            moveDir.z -= 1;
            //transform.position += new Vector3(0, 0, -moveSpeed);
        }
        if (Input.GetKey("a"))
        {
            isMoving = true;
            moveDir.x -= 1;
            //transform.position += new Vector3(-moveSpeed, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            isMoving = true;
            moveDir.x += 1;
            //transform.position += new Vector3(moveSpeed, 0, 0);
        }


        if (moveDir.x + moveDir.z < -1 && moveDir.x + moveDir.z > 1)
        {

        }
        else
        {
            moveDir.x *= .66f;
            moveDir.z *= .66f;
        }



        if(isMoveLocalTransform)
        {
            Transform tempCam = GameObject.Find("Camera Pos").transform;

            tempLocalObject.position = transform.position;
            tempLocalObject.rotation = tempCam.transform.rotation;
            //tempLocalObject.localPosition += new Vector3(moveDir.x, 0, moveDir.z) * moveSpeed;
            tempLocalObject.position += tempLocalObject.rotation * Vector3.forward * moveDir.z * moveSpeed;
            tempLocalObject.position += tempLocalObject.rotation * Vector3.right * moveDir.x * moveSpeed;
            transform.position = tempLocalObject.position;

            //force look at boss
            Vector3 lookDirection = Vector3.zero;

            /*if (nearestTarget)
            {
                lookDirection = nearestTarget.transform.position - skin.transform.position;
            }else
            {
                lookDirection = GameObject.Find("Boss_001").transform.position - skin.transform.position;
            }*/

            /*lookDirection = GameObject.Find("Boss_001").transform.position - skin.transform.position;
            lookDirection.Normalize();

            skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, Quaternion.LookRotation(lookDirection), rotSpeed * Time.deltaTime);*/
        }
        else
        {
            transform.position += moveDir * moveSpeed;
        }

        
    }
    void AvatarAA()
    {
        if(!isAttacking)
        {
            if (nearestTarget && !isCastingSkill)
            {
                if (!isAACharging)
                {
                    avatarAA.SetActive(true);
                    avatarAA.GetComponent<PlayableDirector>().playableAsset = AACharge;
                    avatarAA.GetComponent<PlayableDirector>().Play();
                    isAACharging = true;

                    //print("Start Charge AA");
                }
                avatarAA.transform.Find("target Pos").transform.position = nearestTarget.transform.position;


                if (!isMoving && !isDash)
                {
                    if (isAAReady)
                    {
                        avatarAA.GetComponent<PlayableDirector>().playableAsset = AAStart;
                        avatarAA.GetComponent<PlayableDirector>().Play();
                        isAACharging = false;
                        isAttacking = true;
                        isAAReady = false;

                    }
                }
            }
            else
            {
                avatarAA.SetActive(false);
                isAACharging = false;
                isAAReady = false;
                //print("End Charge AA");
            }
        }


    }

    void UpdateAnim(string animState)
    {
        if (currentAnimState != animState)
        {
            anim.CrossFade(animState, .2f);
            currentAnimState = animState;
        }
    }

    void StartDash()
    {
        isDash = true;

    }

    public void FindNearestTarget()
    {
        GameObject oldtarget = nearestTarget;
        //clean up missing object
        for (int i = 0; i < targets.Count; i++) 
        {
            if (targets[i] == null || targets[i].gameObject.tag != "Enemy")
                targets.RemoveAt(i);

        }

        float tempDist = 0;
        if(targets.Count > 0)
        {
            nearestTarget = targets[0];
            tempDist = Vector3.Distance(transform.position, targets[0].transform.position);

            for (int i = 0; i < targets.Count; i++) 
            {
                if (Vector3.Distance(transform.position, targets[i].transform.position) < tempDist)
                {
                    nearestTarget = targets[i];
                    tempDist = Vector3.Distance(transform.position, nearestTarget.transform.position);
                    //print("update target: " + nearestTarget.name);
                }
                    
            }

            cameraSmoothFollow.playerTarget = nearestTarget.transform;
            targetRange.color = new Color(1, 1, 1, .25f);

            //show target line
            if(oldtarget != nearestTarget)
            {
                drawLineRenderer.Point3 = nearestTarget.transform;
                drawLineRenderer.isActive = true;
                drawLineRenderer.ActiveArrow();
            }
            
        }
        else
        {
            nearestTarget = null;
            cameraSmoothFollow.playerTarget = null;
            targetRange.color = new Color(0, 0, 0, .25f);

            //disable target line
            drawLineRenderer.isActive = false;
        }

        
    }

    public void SignalAvatarAAChargeEnd()
    {
        avatarAA.GetComponent<AudioSource>().PlayOneShot(SFXAAChargeEnd);
        isAAReady = true;
    }

    public void SignalAvatarAAEnd()
    {
        isAttacking = false;
        FindNearestTarget();
    }

    public void SignalAAProjectile()
    {
        GameObject projectile = Instantiate(AAProjectile);
        projectile.transform.position = transform.position;
        projectile.transform.Find("Ball").GetComponent<AvatarAAProjectile>().targetPos = nearestTarget.transform;

    }

    public void SignalAvatarAAImpact()
    {
        GameObject impact = Instantiate(VFXAAExplosion);
        impact.transform.position = avatarAA.transform.Find("target Pos").transform.position;
        Destroy(impact, 2);
    }

    void AvatarDashSystem()
    {
        if (dashCurrentProgess < dashMaxProgress)
        {
            dashCurrentProgess += Time.deltaTime * dashProgressSpeed;
        }
        else
            dashCurrentProgess = dashMaxProgress;


        dashProgressBar.fillAmount = dashCurrentProgess/4/100;

        dashProgressSlot1.SetActive(false);
        dashProgressSlot2.SetActive(false);

        if (dashCurrentProgess >= 50)
            dashProgressSlot1.SetActive(true);

        if (dashCurrentProgess >= 100)
            dashProgressSlot2.SetActive(true);
    }

    public void BeingHeal(int value)
    {
        currentHp += value;
        if (currentHp > MaxHp)
            currentHp = MaxHp;

        if(apostleHp)
            apostleHp.UpdateHpBar(currentHp * 1f / MaxHp);
    }

}
