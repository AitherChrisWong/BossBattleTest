using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Obsolete("Use AxPlanetDetailPvp_TabSelection instead")]
public class ChrisSamplePlanetDetailsLeftToggles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Toggle _Toggle;
    Animator anim;
    public GameObject selectLight;
    ChrisSampleCanvasPlanetDetailsS3 canvasPlanetDetailsS3;

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
        if(!_Toggle.isOn)
            anim.Play("sharing_btn_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_Toggle.isOn)
            anim.Play("sharing_btn_idle");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.Play("sharing_btn_trigger");

    }

    public void ToggleOnPress(int id)
    {
        canvasPlanetDetailsS3.ShowRightContent(id);
    }
}
