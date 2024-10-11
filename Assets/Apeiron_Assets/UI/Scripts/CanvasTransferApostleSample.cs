using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTransferApostleSample : MonoBehaviour
{
    public Transform[] ReserveSlots;
    public int currentSelectedSlotId;
    private int slotCount = 6;

    public Transform reserveGroup;

    public Transform reserveContentLeft;
    public Transform reserveContentCenter;
    public Transform reserveContentRight;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSlotPosition(currentSelectedSlotId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnReserveSlotPointerIn(Animator anim)
    {
        if(int.Parse(anim.gameObject.name) != currentSelectedSlotId)
            anim.Play("ApostleReserveSlot_hover");
        

        print("mouse hover slot: " + int.Parse(anim.gameObject.name));
    }

    public void btnReserveSlotPointerOut(Animator anim)
    {
        if (int.Parse(anim.gameObject.name) != currentSelectedSlotId)
            anim.Play("ApostleReserveSlot_idle");
    }

    public void btnReserveSlotPointerClick(Animator anim)
    {
        if (int.Parse(anim.gameObject.name) != currentSelectedSlotId)
        {
            anim.Play("ApostleReserveSlot_select");
            UpdateSlotPosition(int.Parse(anim.gameObject.name));

            currentSelectedSlotId = int.Parse(anim.gameObject.name);
        }
            
    }

    void UpdateSlotPosition(int tempId)
    {
        for(int i = 0; i < slotCount; i++)
        {
            if(i< tempId)
            {
                ReserveSlots[i].SetParent(reserveContentLeft);
                ReserveSlots[i].GetComponent<Animator>().Play("ApostleReserveSlot_idle");
            }
            else if( i == tempId)
            {
                ReserveSlots[i].SetParent(reserveContentCenter);
                ReserveSlots[i].GetComponent<Animator>().Play("ApostleReserveSlot_select");
            }
            else
            {
                ReserveSlots[i].SetParent(reserveContentRight);
                ReserveSlots[i].transform.SetSiblingIndex(0);
                ReserveSlots[i].GetComponent<Animator>().Play("ApostleReserveSlot_idle");
            }
        }

        StartCoroutine(StartUpdateLayoutGroup());

    }

    IEnumerator StartUpdateLayoutGroup()
    {
        yield return new WaitForSeconds(.01f);

        reserveGroup.GetComponent<HorizontalLayoutGroup>().enabled = false;
        reserveGroup.GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    /*IEnumerator UpdateLayoutGroupCounter(float time)
    {
        reserveGroup.
    }*/
}
