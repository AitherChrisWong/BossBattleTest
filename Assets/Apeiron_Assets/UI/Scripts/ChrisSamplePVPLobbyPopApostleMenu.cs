using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisSamplePVPLobbyPopApostleMenu : MonoBehaviour
{
    public ChrisSamplePVPLobbyController _PVPLobbyController;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnChangeOnPress(int id)
    {
        _PVPLobbyController.SetSelectingApostleId(id);
        GetComponent<Animator>().Play("popup_menu_out");
        _PVPLobbyController.ActiveChangeApostleUi();
        _PVPLobbyController.ChangeApostleMode(false);

        switch (id)
        {
            case 0:
                break;

            case 1:
                _PVPLobbyController.UpdateCameraAnimation("PVPLobby_camera_apostle_L1");
                break;

            case 2:
                _PVPLobbyController.UpdateCameraAnimation("PVPLobby_camera_apostle_R1");
                break;

            case 3:
                _PVPLobbyController.UpdateCameraAnimation("PVPLobby_camera_apostle_L2");
                break;

            case 4:
                _PVPLobbyController.UpdateCameraAnimation("PVPLobby_camera_apostle_R2");
                break;

            case 5:
                _PVPLobbyController.UpdateCameraAnimation("PVPLobby_camera_apostle_L3");
                break;

            case 6:
                _PVPLobbyController.UpdateCameraAnimation("PVPLobby_camera_apostle_R3");
                break;


        }
    }
}
