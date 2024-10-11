using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.UI;


public class ApostleHp : MonoBehaviour
{
    public Transform hpBar;
    public Transform hpBarRed;

    public Transform shieldBar;

    Animator anim;

    public Image hpBarRedLight;
    //public float hpPercentage;

    // Start is called before the first frame update
    void Start()
    {
        //hpPercentage = 100;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hpBar.localScale != hpBarRed.localScale)
        {
            hpBarRed.localScale = Vector3.MoveTowards(hpBarRed.localScale, hpBar.localScale, .005f);
        }

        var tempColor = hpBarRedLight.color;
        if (tempColor.a > 0)
        {
            tempColor.a -= .3f;
            hpBarRedLight.color = tempColor;
        }
    }

    public void UpdateHpBar(float value)
    {
        hpBar.transform.localScale = new Vector3(value, 1, 1);
        var newColor = hpBarRedLight.color;
        newColor.a = 1;
        hpBarRedLight.color = newColor;
    }

    public void UpdateStaggerBar(float value)
    {
        if(shieldBar)
            shieldBar.transform.localScale = new Vector3(value, 1, 1);
    }

    public void ApostleHpDie()
    {
        anim.Play("CanvasApostleHPDie");
    }
}
