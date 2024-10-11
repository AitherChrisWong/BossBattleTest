using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChrisSampleCanvasSelectPlanetFilter : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnFilterOnPress(Toggle change)
    {
        if(change.isOn)
        {
            anim.Play("sharing_toggle_trigger");
            
        }
        else
        {
            anim.Play("sharing_toggle_idle");
        }


    }


}
