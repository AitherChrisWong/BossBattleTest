using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisSamplePVPLobbyController : MonoBehaviour
{
    public Animator camAnim;
    public GameObject canvasCanvasChangeApostle;
    public GameObject canvasMatching;
    public GameObject canvasReservedRelicApostleList;
    public GameObject canvasSelectPlanet;

    public int selectingApostleSlotId = 0;

    public GameObject[] changeApostleDemoWillDisableObjects;
    public GameObject[] changeMatchingWillDisableObjects;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCameraAnimation(string animState)
    {
        camAnim.Play(animState);
    }

    public void ActiveChangeApostleUi()
    {
        canvasCanvasChangeApostle.SetActive(true);
        canvasCanvasChangeApostle.GetComponent<Animator>().Play("CanvasChangeApostle_active");
    }

    public void SetSelectingApostleId(int id)
    {
        selectingApostleSlotId = id;
    }

    public void ChangeApostleMode(bool isShow)
    {
        foreach(GameObject g in changeApostleDemoWillDisableObjects)
        {
            g.SetActive(isShow);
        }
    }

    public void ChangeMatchingMode(bool isShow)
    {
        foreach (GameObject g in changeMatchingWillDisableObjects)
        {
            g.SetActive(isShow);
        }
    }

    public void ResetCamera()
    {
        switch(selectingApostleSlotId)
        {
            case 0:
                break;

            case 1:
                camAnim.Play("PVPLobby_camera_apostle_L1_back");
                break;

            case 2:
                camAnim.Play("PVPLobby_camera_apostle_R1_back");
                break;

            case 3:
                camAnim.Play("PVPLobby_camera_apostle_L2_back");
                break;

            case 4:
                camAnim.Play("PVPLobby_camera_apostle_R2_back");
                break;

            case 5:
                camAnim.Play("PVPLobby_camera_apostle_L3_back");
                break;

            case 6:
                camAnim.Play("PVPLobby_camera_apostle_R3_back");
                break;


        }
    }

    public void StartMatching()
    {
        canvasMatching.SetActive(true);
        canvasMatching.GetComponent<Animator>().Play("match_in");

        ChangeMatchingMode(false);
    }

    public void BtnApostleListOnPress()
    {
        canvasReservedRelicApostleList.SetActive(true);
        canvasReservedRelicApostleList.GetComponent<Animator>().Play("active");
    }

    public void BtnSelectPlanetOnPress()
    {
        canvasSelectPlanet.SetActive(true);
        canvasSelectPlanet.GetComponent<Animator>().Play("active");
    }
}
