using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DamageText : MonoBehaviour
{
    //public int value;

    public bool isStraggerText;

    public Transform target;

    public TextMeshProUGUI txtDamage;
    public TextMeshProUGUI txtDamage2;

    public enum Type1 { avatar, apostle, enemy }
    public enum Type2 { magicDamage, PhysicDamage, RealDamage }

    //public Type1 _type1 = Type1.avatar;
    //public Type2 _type2 = Type2.RealDamage;

    public Color avatarColor;
    public Color apostleColor;
    public Color enemyColor;
    public Color allyHealColor;
    public Color enemyHealColor;

    public Material mtlMagicDamage;
    public Material mtlPhysicDamage;
    public Material mtlRealDamage;

    Vector3 randomOffset;
    public float offsetRange = 10;



    // Start is called before the first frame update
    void Start()
    {
        randomOffset = new Vector3(Random.Range(-offsetRange, offsetRange), Random.Range(-offsetRange, offsetRange),0);

        if(isStraggerText)
            GetComponent<Animator>().Play("DamageFloatingText_Stragger", -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (target) //keep update text position
        {
            RectTransform canvasDamage = transform.root.GetComponent<RectTransform>();

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
            Vector3 tempPos = screenPoint - canvasDamage.sizeDelta / 2f;
            tempPos += randomOffset;

            transform.localPosition = tempPos;
        }
        

    }

    public void UpdateDamageText(int value, Type1 _type1, Type2 _type2)
    {
        txtDamage.text = value.ToString();
        txtDamage2.text = value.ToString();

        switch (_type1)
        {
            case Type1.avatar:
                txtDamage.fontSize *= 1.5f;
                txtDamage2.fontSize *= 1.5f;
                txtDamage.color = avatarColor;
                txtDamage2.color = avatarColor;
                break;

            case Type1.apostle:
                txtDamage.color = apostleColor;
                txtDamage2.color = apostleColor;
                break;

            case Type1.enemy:
                txtDamage.color = enemyColor;
                txtDamage2.color = enemyColor;
                break;

        }

        switch(_type2)
        {
            case Type2.magicDamage:
                txtDamage.material = mtlMagicDamage;
                txtDamage2.material = mtlMagicDamage;
                break;

            case Type2.PhysicDamage:
                txtDamage.material = mtlPhysicDamage;
                txtDamage2.material = mtlPhysicDamage;
                break;

            case Type2.RealDamage:
                txtDamage.material = mtlRealDamage;
                txtDamage2.material = mtlRealDamage;
                break;
        }
    }

    public void UpdateHealText(int value, Type1 _type1)
    {
        txtDamage.text = value.ToString();
        txtDamage2.text = value.ToString();

        switch (_type1)
        {
            case Type1.avatar:
                txtDamage.color = allyHealColor;
                txtDamage2.color = allyHealColor;
                break;

            case Type1.apostle:
                txtDamage.color = allyHealColor;
                txtDamage2.color = allyHealColor;
                break;

            case Type1.enemy:
                txtDamage.color = enemyHealColor;
                txtDamage2.color = enemyHealColor;
                break;

        }
    }
}
