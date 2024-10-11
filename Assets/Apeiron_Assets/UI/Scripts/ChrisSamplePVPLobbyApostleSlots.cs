using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChrisSamplePVPLobbyApostleSlots : MonoBehaviour
{
    public GameObject criclePopupMenu;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        //make sure mask to ui
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            criclePopupMenu.SetActive(true);
            criclePopupMenu.GetComponent<Animator>().Play("popup_menu_in", -1, 0);
            //print("current NOT point to ui");
        }
    }


}
