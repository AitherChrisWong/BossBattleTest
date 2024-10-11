using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class ApostleMovement : MonoBehaviour
{
    public int currentHp;
    public int MaxHp;
    ApostleHp apostleHp;

    public bool isAlly;
    public bool isAttacking;
    public bool isMoving;
    public bool isAlive;

    public float attackRange = 1;

    public float moveSpeed;
    [HideInInspector] public Vector3 moveDirection;
    public Transform dirCube;
    public DrawLineRenderer drawLineRenderer;


    [Header("Skin Setting")]
    public Transform skin;
    public Animator anim;
    string currentAnimState = null;

    public float rotSpeed = 10;

    [Header("Target System")]
    public GameObject nearestTarget;
    public List<GameObject> targets = new List<GameObject>();

    [Header("VFX Timeline")]
    public int AAComboId = 0;
    public GameObject AA1;
    public GameObject AA2;


    [Header("Being Attack")]

    public AnimationCurve beingAtkLightCurve;
    public float currentBeingAttackLightTime;
    public float benigAtkLightSpeed;

    public bool isBeingAttack = false;
    public SkinnedMeshRenderer[] BodyMesh;

    public GameObject VFXImpact;


    Rigidbody rb;

    public GameObject txtDamage;
    RectTransform canvasDamage;

    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
        rb = GetComponent<Rigidbody>();
        apostleHp = transform.Find("CanvasApostleHP").GetComponent<ApostleHp>();
        canvasDamage = GameObject.Find("CanvasDamageNum").GetComponent<RectTransform>();

        currentHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (isAlive)
        {
            if (!isAttacking) // dont move when attacking
            {
                FindTarget();

                if (nearestTarget != null)
                {
                    if (Vector3.Distance(transform.position, nearestTarget.transform.position) > attackRange)
                    {
                        MoveToTarget(nearestTarget.transform.position);
                    }
                }

            }

            if (!isMoving && !isAttacking) //do something if not moving
            {
                switch (AAComboId)
                {
                    case 0:
                        transform.LookAt(nearestTarget.transform.position);
                        StartAA1();
                        AAComboId = 1;
                        break;

                    case 1:
                        transform.LookAt(nearestTarget.transform.position);
                        StartAA2();
                        AAComboId = 0;
                        break;
                }
            }

            if (isMoving)
            {
                UpdateAnim("move");
            }
            else
            {
                UpdateAnim("idle");
            }

            isMoving = false;

            if (isBeingAttack)
            {
                if (currentBeingAttackLightTime < 1)
                {
                    foreach (SkinnedMeshRenderer mesh in BodyMesh)
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
        

    }

    void MoveToTarget(Vector3 pos)
    {
        isMoving = true;
        transform.position = Vector3.Lerp(transform.position, pos, moveSpeed);
        transform.LookAt(pos);
    }

    void FindTarget()
    {
        GameObject oldtarget = nearestTarget;
        nearestTarget = null;
        targets.Clear();

        GameObject[] tempEnemy;
        if (isAlly)
        {
            tempEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else
        {
            tempEnemy = GameObject.FindGameObjectsWithTag("Player");
        }
        

        foreach (GameObject e in tempEnemy)
        {
            targets.Add(e);
        }

        float tempDist = 0;
        if (!nearestTarget)
        {
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
            }
        }

        //show target line if new target
        if (oldtarget != nearestTarget)
        {
            drawLineRenderer.Point3 = nearestTarget.transform;
            drawLineRenderer.isActive = true;
            drawLineRenderer.ActiveArrow();
        }

    }

    void UpdateAnim(string animState)
    {
        if (currentAnimState != animState)
        {
            anim.CrossFade(animState, .1f);
            currentAnimState = animState;
        }

    }

    void StartAA1()
    {
        isAttacking = true;
        AA1.SetActive(true);
    }
    void StartAA2()
    {
        isAttacking = true;
        AA2.SetActive(true);
    }

    public void SignalAttackEnd()
    {
        isAttacking = false;
        FindTarget();
        //isLockedTarget = false;

        //isSkill2LerpToTarget = false;
    }

    public void SignalAttackDamage()
    {
        int _damage = 75;

        //attacking boss
        if (nearestTarget.TryGetComponent<BossControl>(out BossControl bossControl))
        {
            

            if (bossControl.isSuperStun)
            {
                _damage = Mathf.RoundToInt(_damage * 2.5f);
                CreateDamageText(_damage, true);
            }
            else
                CreateDamageText(_damage, false);

            bossControl.StartBeingAtkLight();
            bossControl.BeingAttack(_damage);

        }

        if (nearestTarget.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
        {
            CreateDamageText(_damage, false);
            apostleMovement.StartBeingAtkLight();
            apostleMovement.BeingAttack(_damage);
            
        }

        if(nearestTarget.name == "VFX_Boss_fireball")
        {
            CreateDamageText(_damage, false);
            nearestTarget.GetComponent<BossFireballController>().StartDestroy();
        }
    }

    void CreateDamageText(int damage, bool isStragger)
    {
        
        GameObject tempText = Instantiate(txtDamage, canvasDamage);

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, nearestTarget.transform.position);
        Vector3 tempPos = screenPoint - canvasDamage.sizeDelta / 2f;
        tempText.transform.localPosition = tempPos;

        if(isAlly)
            tempText.GetComponent<DamageText>().UpdateDamageText(damage, DamageText.Type1.apostle, DamageText.Type2.PhysicDamage);
        else
            tempText.GetComponent<DamageText>().UpdateDamageText(damage, DamageText.Type1.enemy, DamageText.Type2.PhysicDamage);

        tempText.GetComponent<DamageText>().target = nearestTarget.transform;

        tempText.GetComponent<DamageText>().isStraggerText = isStragger;


        Destroy(tempText, 1);
    }


    public void StartBeingAtkLight()
    {
        isBeingAttack = true;
        currentBeingAttackLightTime = 0;

        GameObject tempimpact = Instantiate(VFXImpact, transform);
        tempimpact.transform.localPosition = new Vector3(0, 1, 0);

    }

    public void BeingAttack(int value)
    {
        currentHp -= value;
        apostleHp.UpdateHpBar(currentHp * 1f / MaxHp);

        if (currentHp <= 0)
        {
            isAlive = false;
            tag = "Untagged";
            GetComponent<CapsuleCollider>().enabled = false;
            UpdateAnim("die");
            Destroy(this.gameObject, 5);

            apostleHp.ApostleHpDie();
        }


    }
}
