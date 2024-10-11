using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Obsolete("AxPlanetDetailPvp_PlanetSelection")]
public class ChrisSamplePlanetDetailsBtnPlanet : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    ChrisSampleCanvasPlanetDetailsS3 canvasPlanetDetailsS3;

    Toggle _Toggle;
    Animator anim;
    public GameObject selectLight;
    // Start is called before the first frame update
    void Start()
    {
        _Toggle = GetComponent<Toggle>();
        _Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_Toggle);
        });

        anim = GetComponent<Animator>();
        canvasPlanetDetailsS3 = GameObject.Find("CanvasPlanetDetails(S3)").GetComponent<ChrisSampleCanvasPlanetDetailsS3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleValueChanged(Toggle change)
    {
        //print("change:" + change.isOn);
        selectLight.SetActive(change.isOn);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.Play("sharing_btn_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.Play("sharing_btn_idle");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.Play("sharing_btn_trigger");

        if(canvasPlanetDetailsS3.isPlanetViewExpend)
        {
            canvasPlanetDetailsS3.BtnViewMoreOnPress();
        }
    }
}
