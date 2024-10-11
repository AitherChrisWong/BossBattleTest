using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.Playables;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public int currentHp;
    public int MaxHp = 10000;

    public int currentStagger;
    public int maxStagger = 5000;
    public ApostleHp apostleHp;

    public Transform target;
    public DrawLineRenderer drawLineRenderer;

    [Header("Status")]
    public bool isSuperStun;
    public bool isMoving;
    public bool isAttacking;
    public bool isLockedTarget;


    public float attackRange = 3;

    public float moveSpeed = .1f;

    public Transform skin;
    public Animator anim;
    string currentAnimState = null;
    public float rotSpeed = 1;

    [Header("Skill value")]
    //AA4
    public bool isAA4Dash;
    public float BossAA4DashSpeed;
    Vector3 lookDirection = Vector3.zero;

    //Skill2
    public bool isSkill2LockTarget;
    public Transform skill2TargetRange;
    public bool isSkill2LerpToTarget;
    public Vector3 skill2JumpToTargetPos;
    public float skill2JumpSpeed = .1f;

    //Skill4
    public bool isSkill4JumpBack;
    public float BossSkill4JumpSpeed;
    public bool isSkill4PushBack;
    public float BossSkill4PushSpeed;
    //Vector3 lookDirection = Vector3.zero;

    [Header("VFX Timeline")]
    public GameObject AA1;
    public float AA1CDTime = 5;
    public float AA1CurrentCDTime = 0;
    public GameObject AA2;
    public float AA2CDTime = 5;
    public float AA2CurrentCDTime = 0;
    public GameObject AA3;
    public float AA3CDTime = 5;
    public float AA3CurrentCDTime = 0;
    public GameObject AA4;
    public float AA4CDTime = 5;
    public float AA4CurrentCDTime = 0;
    
    public GameObject Skill1;
    public float Skill1CDTime = 5;
    public float Skill1CurrentCDTime = 0;
    public GameObject Skill2;
    public float Skill2CDTime = 5;
    public float Skill2CurrentCDTime = 0;
    public GameObject Skill3;
    public float Skill3CDTime = 5;
    public float Skill3CurrentCDTime = 0;
    public GameObject Skill4;
    public float Skill4CDTime = 5;
    public float Skill4CurrentCDTime = 0;

    public GameObject SuperStun;

    [Header("Being Attack")]
    
    public AnimationCurve beingAtkLightCurve;
    public float currentBeingAttackLightTime;
    public float benigAtkLightSpeed;

    public bool isBeingAttack = false;
    public SkinnedMeshRenderer[] BossMesh;

    public GameObject VFXImpact;

    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //apostleHp = transform.Find("CanvasApostleHP").GetComponent<ApostleHp>();

        currentHp = MaxHp;

        if (target)
            ActiveTargetLine();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            BossStartStagger();
        }

        if (!isSuperStun)
        {
            rb.velocity = Vector3.zero;

            if (Input.GetKeyDown("p"))
            {
                StartBeingAtkLight();
            }

            if (!isAttacking) // dont move when attacking
            {
                if (Vector3.Distance(transform.position, target.position) > attackRange)
                {
                    MoveToTarget(target.position);
                }
            }

            if (!isMoving)  //do something if not moving
            {
                //Skill 1 - summon
                if (Skill1CurrentCDTime <= 0 && !isAttacking)
                {
                    StartSkill1();
                }

                if (AA3CurrentCDTime <= 0 && !isAttacking)
                {
                    StartAA3();
                }

                if (AA2CurrentCDTime <= 0 && !isAttacking)
                {
                    StartAA2();
                }

                if (AA1CurrentCDTime <= 0 && !isAttacking)
                {
                    StartAA1();
                }


                //skill2 - jump
                if (Skill2CurrentCDTime <= 0 && !isAttacking)
                {
                    StartSkill2();
                }

                //skill3 - big swing
                if (Skill3CurrentCDTime <= 0 && !isAttacking)
                {
                    StartSkill3();
                }

                //Skill4 - map cannon
                if (Skill4CurrentCDTime <= 0 && !isAttacking)
                {
                    StartSkill4();
                }
            }
            else //long distance attack
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance > attackRange + 1)
                {
                    //Skill4 - map cannon
                    if (Skill4CurrentCDTime <= 0 && !isAttacking)
                    {
                        StartSkill4();
                    }

                    /*
                    //Skill 1 - summon
                    if (Skill1CurrentCDTime <= 0 && !isAttacking)
                    {
                        StartSkill1();
                    }*/

                    if (AA4CurrentCDTime <= 0 && !isAttacking)
                    {
                        StartAA4();
                    }

                    //skill2 - jump
                    if (Skill2CurrentCDTime <= 0 && !isAttacking)
                    {
                        StartSkill2();
                    }

                    //Skill4 - map cannon
                    if (Skill4CurrentCDTime <= 0 && !isAttacking)
                    {
                        StartSkill4();
                    }
                }
            }


            CheckBossSkillMovement();

            if (isSkill2LockTarget)
            {
                skill2TargetRange.position = target.position;
            }
            else
            {
                skill2TargetRange.position = skill2JumpToTargetPos;
            }



            AA1CurrentCDTime -= Time.deltaTime; if (AA1CurrentCDTime < 0) AA1CurrentCDTime = 0;
            AA2CurrentCDTime -= Time.deltaTime; if (AA2CurrentCDTime < 0) AA2CurrentCDTime = 0;
            AA3CurrentCDTime -= Time.deltaTime; if (AA3CurrentCDTime < 0) AA3CurrentCDTime = 0;
            AA4CurrentCDTime -= Time.deltaTime; if (AA4CurrentCDTime < 0) AA4CurrentCDTime = 0;
            Skill1CurrentCDTime -= Time.deltaTime; if (Skill1CurrentCDTime < 0) Skill1CurrentCDTime = 0;
            Skill2CurrentCDTime -= Time.deltaTime; if (Skill2CurrentCDTime < 0) Skill2CurrentCDTime = 0;
            Skill3CurrentCDTime -= Time.deltaTime; if (Skill3CurrentCDTime < 0) Skill3CurrentCDTime = 0;
            Skill4CurrentCDTime -= Time.deltaTime; if (Skill4CurrentCDTime < 0) Skill4CurrentCDTime = 0;


            //lock boss rotation when attack
            if (!isLockedTarget)
            {
                Vector3 lookDirection = target.position - skin.transform.position;
                lookDirection.Normalize();

                skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, Quaternion.LookRotation(lookDirection), rotSpeed * Time.deltaTime);
            }



            if (isMoving)
            {
                UpdateAnim("Boss001_Move");
            }
            else
            {
                UpdateAnim("Boss001_Idle");
            }

            isMoving = false;

            //print(anim.GetCurrentAnimatorStateInfo(0).shortNameHash);
        }

        if (isBeingAttack)
        {
            if (currentBeingAttackLightTime < 1)
            {
                foreach (SkinnedMeshRenderer mesh in BossMesh)
                {
                    mesh.material.SetFloat("_BeingAttackLight", beingAtkLightCurve.Evaluate(currentBeingAttackLightTime));
                }

                currentBeingAttackLightTime += Time.deltaTime * benigAtkLightSpeed;
            }
            else
            {
                isBeingAttack = false;

            }
        }


    }

    void MoveToTarget(Vector3 pos)
    {
        isMoving = true;
        transform.position = Vector3.Lerp(transform.position, pos, moveSpeed);
    }

    void UpdateAnim(string animState)
    {
        if (currentAnimState != animState)
        {
            anim.CrossFade(animState, .1f);
            currentAnimState = animState;
        }
            
    }

    void BossStartStagger()
    {
        //force stop all attack
        AA1.SetActive(false);
        AA2.SetActive(false);
        AA3.SetActive(false);
        AA4.SetActive(false);
        Skill1.SetActive(false);
        Skill2.SetActive(false);
        Skill3.SetActive(false);
        Skill4.SetActive(false);

        //hoxfix skill 2 lerp problem
        Vector3 tempPos = anim.transform.localPosition;
        tempPos.y = 0;
        anim.transform.localPosition = tempPos;

        isSkill4JumpBack = false;
        isSkill4PushBack = false;

        isSuperStun = true;
        SuperStun.SetActive(true);

        isMoving = false;
        isAttacking = false;
        isLockedTarget = false;
    }
    

    void StartAA1()
    {
        isAttacking = true;
        AA1.SetActive(true);
        AA1CurrentCDTime = AA1CDTime;
    }

    void StartAA2()
    {
        isAttacking = true;
        AA2.SetActive(true);
        AA2CurrentCDTime = AA2CDTime;
    }

    void StartAA3()
    {
        isAttacking = true;
        AA3.SetActive(true);
        AA3CurrentCDTime = AA3CDTime;
    }

    void StartAA4()
    {
        isAttacking = true;
        AA4.SetActive(true);
        AA4CurrentCDTime = AA4CDTime;

        //AA4.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(1.5f);
    }

    void StartSkill1()
    {
        isAttacking = true;
        Skill1.SetActive(true);
        Skill1CurrentCDTime = Skill1CDTime;
    }

    void StartSkill2()
    {
        isAttacking = true;
        Skill2.SetActive(true);
        Skill2CurrentCDTime = Skill2CDTime;
    }

    void StartSkill3()
    {
        isAttacking = true;
        Skill3.SetActive(true);
        Skill3CurrentCDTime = Skill3CDTime;
    }

    void StartSkill4()
    {
        isAttacking = true;
        Skill4.SetActive(true);
        Skill4CurrentCDTime = Skill4CDTime;
    }




    //-------

    public void SignalBossAttackEnd()
    {
        isAttacking = false;
        isLockedTarget = false;

        isSkill2LerpToTarget = false;
    }

    public void SignalBossLockTarget()
    {
        isLockedTarget = true;
    
    
    }

    public void SignalBossAA4DashStart()
    {
        Transform tempDirBox = new GameObject().transform;
        tempDirBox.SetParent(this.transform.Find("skin"));
        tempDirBox.localPosition = new Vector3(0, 0, 3);

        lookDirection = tempDirBox.position - transform.position;
        lookDirection.Normalize();

        isAA4Dash = true;
    }

    public void SignalBossAA4DashEnd()
    {
        isAA4Dash = false;
    }

    public void SignalBossSkill2LockTargetStart()
    {
        isSkill2LockTarget = true;
        skill2TargetRange.position = target.position;
    }

    public void SignalBossSkill2LockTargetEnd()
    {
        isSkill2LockTarget = false;

        skill2JumpToTargetPos = target.position;
        //skill2TargetRange.position = skill2JumpToTargetPos;

    }

    public void SignalBossSkill2LerpToTarget()
    {
        isSkill2LerpToTarget = true;
        capsuleCollider.isTrigger = true;
    }

    public void SignalBossSkill2LerpEnd()
    {
        capsuleCollider.isTrigger = false;
    }

    public void SignalBossSKill4JumpBackStart()
    {
        Transform tempDirBox = new GameObject().transform;
        tempDirBox.SetParent(this.transform.Find("skin"));
        tempDirBox.localPosition = new Vector3(0, 0, 3);

        lookDirection = tempDirBox.position - transform.position;
        lookDirection.Normalize();

        isSkill4JumpBack = true;
    }

    public void SignalBossSKill4JumpBackEnd()
    {
        isSkill4JumpBack = false;
    }

    public void SignalBossSKill4PushBackStart()
    {
        isSkill4PushBack = true;
    }

    public void SignalBossSKill4PushBackEnd()
    {
        isSkill4PushBack = false;
    }

    public void SignalSuperStunEnd()
    {
        isSuperStun = false;
    }



    void CheckBossSkillMovement()
    {
        if (isAA4Dash)
        {
            transform.position += lookDirection * BossAA4DashSpeed;
        }

        if (isSkill2LerpToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, skill2JumpToTargetPos, skill2JumpSpeed);
        }

        if(isSkill4JumpBack)
        {
            transform.position -= lookDirection * BossSkill4JumpSpeed;
        }

        if (isSkill4PushBack)
        {
            var rotatedVector = skin.transform.rotation * Vector3.forward;

            transform.position -= rotatedVector * BossSkill4PushSpeed;
        }
    }

    public void StartBeingAtkLight()
    {
        isBeingAttack = true;
        currentBeingAttackLightTime = 0;

        GameObject tempimpact = Instantiate(VFXImpact, transform);
        tempimpact.transform.localPosition = new Vector3(0, 2.5f, 0);
        tempimpact.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    public void BeingAttack(int value)
    {
        currentHp -= value;
        apostleHp.UpdateHpBar(currentHp * 1f / MaxHp);

        if(!isSuperStun)
        {
            currentStagger += value;
            apostleHp.UpdateStaggerBar(currentStagger * 1f / maxStagger);
        }
        

        if (currentHp <= 0)
        {
            currentHp = MaxHp;
            //isAlive = false;
            //tag = "Untagged";
            //GetComponent<CapsuleCollider>().enabled = false;
            //UpdateAnim("die");
            //Destroy(this.gameObject, 5);

            //apostleHp.ApostleHpDie();
        }

        if(currentStagger > maxStagger)
        {
            BossStartStagger();
            currentStagger = 0;
            apostleHp.UpdateStaggerBar(0);
        }


    }


    void ActiveTargetLine()
    {
        drawLineRenderer.Point3 = target.transform;
        drawLineRenderer.isActive = true;
        drawLineRenderer.ActiveArrow();
    }
}
