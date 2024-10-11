using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    avatar, apostle, boss, bood
}

public class AttackCollision : MonoBehaviour
{
    public AttackType _attackType = AttackType.avatar;

    public int damage = 150;

    public GameObject txtDamage;
    public RectTransform canvasDamage;

    public GameObject VFXImpact;

    [Header("Status")]
    public bool isKnockback;
    public bool isKnockUp;
    public float knockbackForce;

    // Start is called before the first frame update
    void Start()
    {
        canvasDamage = GameObject.Find("CanvasDamageNum").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(_attackType)
        {
            case AttackType.avatar:
                if (other.gameObject.tag == "Enemy")
                {
                    //Boss being attack
                    if (other.TryGetComponent<BossControl>(out BossControl bossControl))
                    {
                        if(bossControl.isSuperStun)
                        {
                            damage = Mathf.RoundToInt(damage * 2.5f);
                            CreateDamageText(other.transform, DamageText.Type1.avatar, DamageText.Type2.magicDamage, true);
                        }  
                        else
                            CreateDamageText(other.transform, DamageText.Type1.avatar, DamageText.Type2.magicDamage, false);

                        bossControl.StartBeingAtkLight();
                        bossControl.BeingAttack(damage);
                        //print("boss being attack");
                    }
                    if (other.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
                    {
                        CreateDamageText(other.transform, DamageText.Type1.avatar, DamageText.Type2.magicDamage, false);

                        apostleMovement.StartBeingAtkLight();
                        apostleMovement.BeingAttack(damage);
                        //print("boss being attack");
                    }
                    else
                    {
                        if (other.gameObject.name == "VFX_Boss_fireball")
                        {
                            other.GetComponent<BossFireballController>().StartDestroy();
                        }

                        print("enemy being attack");
                    }
                }
                break;

            case AttackType.apostle:
                break;

            case AttackType.boss:
                if (other.gameObject.tag == "Player")
                {
                    //Boss being attack
                    if (other.TryGetComponent<AvatarBasicMovement>(out AvatarBasicMovement avatarBasicMovement))
                    {
                        CreateDamageText(other.transform, DamageText.Type1.enemy, DamageText.Type2.PhysicDamage, false);
                        CreateImpact(other.transform);

                        //avatarBasicMovement.StartBeingAtkLight();
                        //print("avatar being attack");
                    }
                    if (other.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
                    {
                        CreateDamageText(other.transform, DamageText.Type1.enemy, DamageText.Type2.PhysicDamage, false);
                        CreateImpact(other.transform);

                        apostleMovement.StartBeingAtkLight();
                        apostleMovement.BeingAttack(damage);
                        //print("boss being attack");
                    }
                    else
                    {
                        /*
                         * if (other.gameObject.name == "VFX_Boss_fireball")
                        {
                            other.GetComponent<BossFireballController>().StartDestroy();
                        }

                        print("enemy being attack");
                        */
                    }
                }
                break;

            case AttackType.bood:
                break;

        }

        
    }

    void CreateImpact(Transform target)
    {
        GameObject tempImpact = Instantiate(VFXImpact, target);
        tempImpact.transform.localPosition = Vector3.zero;
        Destroy(tempImpact, 1);
    }

    void CreateDamageText(Transform target, DamageText.Type1 type1, DamageText.Type2 type2,bool isStragger)
    {

        GameObject tempText = Instantiate(txtDamage, canvasDamage);

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        Vector3 tempPos = screenPoint - canvasDamage.sizeDelta / 2f;
        tempText.transform.localPosition = tempPos;

        tempText.GetComponent<DamageText>().UpdateDamageText(damage, type1, type2);

        tempText.GetComponent<DamageText>().target = target;


        tempText.GetComponent<DamageText>().isStraggerText = isStragger;


        Destroy(tempText, 1);
    }
}
