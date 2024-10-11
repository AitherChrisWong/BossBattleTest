using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChrisSamplePlanetCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    public int planetId = 0;
    ChrisSampleCanvasSelectPlanet canvasSelectPlanet;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        canvasSelectPlanet = GameObject.Find("CanvasSelectPlanet_p").GetComponent<ChrisSampleCanvasSelectPlanet>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnPlanetCardOnPress()
    {
        canvasSelectPlanet.UpdateSelectCard(planetId);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.Play("sharing_btn_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.Play("sharing_btn_idle");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.Play("sharing_btn_hover");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.Play("sharing_btn_trigger");
        BtnPlanetCardOnPress();

    }
}
