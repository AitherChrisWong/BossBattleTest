using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using UnityEngine.Video;

public class PVEBattleController : MonoBehaviour
{
    //public bool isForcePause = false;

    public bool isSlowMode;
    public Volume slowModePostProcessing;

    public GameObject[] playerTeam;
    public GameObject[] enemyTeam;

    public CameraSmoothFollow cameraSmoothFollow;
    public GameObject cutsceneBossHalfHp;


    [Header("Mana")]
    public TextMeshProUGUI txtMana;
    public Transform manaProgressBar;
    public Transform manaBar;
    public Transform tempCostPos;
    public Transform castCostBar;
    public Transform castCostBarRed;

    public int maxMana = 10;
    public int curMana = 0;

    public float curManaProgress;
    public float manaGenSpeed;

    public GameObject vfxUseMana;
    public Transform[] manaGridPos;

    [Header("Boss ForceZoom")]
    public bool isForceZoom;
    public bool isPlayingBossAngryVideo;
    public float forceZoomDuration;
    public float curForceZoomTime;

    public GameObject bossP2HpBar;

    public AudioSource audioSource;
    public AudioClip sfxHeavyHit;
    public AudioSource audioSourceBGM;
    public AudioClip bgmBossPhase2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowMode)
        {
            slowModePostProcessing.weight = Mathf.Lerp(slowModePostProcessing.weight, 1, .2f);

            Time.timeScale = Mathf.Lerp(Time.timeScale, .02f, .05f);
        }
        else
        {
            slowModePostProcessing.weight = Mathf.Lerp(slowModePostProcessing.weight, 0, .2f);

            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, .5f);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            BossGoToPhase2();
        }

        AutoGenMana();

        if (isForceZoom)
        {
            if (curForceZoomTime < forceZoomDuration)
            {
                curForceZoomTime += Time.unscaledDeltaTime;
            }
            else
            {
                EndForceZoom();
                //AllCharacterForcePause(false);
                cutsceneBossHalfHp.SetActive(true);
                isPlayingBossAngryVideo = true;
                bossP2HpBar.SetActive(true);
            }
        }

        if(isPlayingBossAngryVideo)
        {
            VideoPlayer video = cutsceneBossHalfHp.GetComponent<PVEBossVideoPlayer>().videoPlayer;
            audioSourceBGM.Stop();
            //print(video.frame + "/" + video.frameCount);


            if ((ulong)video.frame == video.frameCount-1)
            {

                cutsceneBossHalfHp.SetActive(false);
                isPlayingBossAngryVideo = false;

                AllCharacterForcePause(false);

                GameObject.Find("Boss_001").GetComponent<BossControl>().StartAngry();

                audioSourceBGM.clip = bgmBossPhase2;
                audioSourceBGM.Play();
            }

            
        }
    }

    public void BossGoToPhase2()
    {
        curForceZoomTime = 0;
        GameObject.Find("Boss_001").GetComponent<BossControl>().BossStartStagger();
        StartForceZoom(GameObject.Find("Boss_001").transform);

        AllCharacterForcePause(true);

        audioSource.PlayOneShot(sfxHeavyHit);

        foreach(Transform t in GameObject.Find("VFX group").transform)
        {
            Destroy(t.gameObject, .5f);
        }
    }

    public void ShowCost(int cost, bool isActive)
    {
        //castCostBar.gameObject.SetActive(isActive);

        castCostBar.gameObject.SetActive(false);
        castCostBarRed.gameObject.SetActive(false);

        if (isActive)
        {
            if (cost <= curMana)
            {
                castCostBar.gameObject.SetActive(true);
                castCostBar.position = tempCostPos.position;
                castCostBar.localScale = new Vector3(cost * .1f, 1, 1);
            }
            else
            {
                castCostBarRed.gameObject.SetActive(true);
                castCostBarRed.localScale = new Vector3(cost * .1f, 1, 1);
            }

        }

        
    }

    void AutoGenMana()
    {
        if(curManaProgress < maxMana)
        {
            curManaProgress += manaGenSpeed * Time.deltaTime;
            curMana = (int)Mathf.Floor(curManaProgress);

            txtMana.text = curMana.ToString();
            manaProgressBar.localScale = new Vector3(curManaProgress / maxMana, 1, 1);
            manaBar.localScale = new Vector3(curMana*1f / maxMana, 1, 1);
            
        }
        
    }

    public void UseMana(int value)
    {
        curManaProgress -= value;

        for (int i = 0; i < value; i++)
        {
            if(curMana - i >= 0)
            {
                GameObject tempVfx = Instantiate(vfxUseMana, vfxUseMana.transform.parent);
                tempVfx.SetActive(true);
                tempVfx.transform.SetParent(manaGridPos[curMana - i - 1]);
                tempVfx.transform.localPosition = Vector3.zero;
                tempVfx.transform.localRotation = Quaternion.identity;

                Destroy(tempVfx, 1);
            }
            

        }
    }

    public void StartForceZoom(Transform target)
    {
        cameraSmoothFollow.targetFollower = GameObject.Find("Boss_001").transform;
        cameraSmoothFollow.transform.position = GameObject.Find("Boss_001").transform.position;
        cameraSmoothFollow.camZoomLevel = -10f;
        cameraSmoothFollow.camShakeAnim.Play("cameraShakeLarge", -1, 0);
        isForceZoom = true;
        isSlowMode = true;
    }

    void EndForceZoom()
    {
        cameraSmoothFollow.targetFollower = playerTeam[0].transform;
        cameraSmoothFollow.camZoomLevel = -20f;
        isForceZoom = false;
        isSlowMode = false;
    }

    public void AllCharacterForcePause(bool isValue)
    {
        enemyTeam = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemyTeam)
        {
            if(enemy.TryGetComponent<BossControl>(out BossControl bossControl))
            {
                bossControl.isForcePause = isValue;
            }

            if (enemy.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
            {
                apostleMovement.isForcePause = isValue;
            }
        }

        foreach(var player in playerTeam)
        {
            if(player!=null)
            {
                if (player.TryGetComponent<AvatarBasicMovement>(out AvatarBasicMovement avatarBasicMovement))
                {
                    avatarBasicMovement.isForcePause = isValue;
                }

                if (player.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
                {
                    apostleMovement.isForcePause = isValue;
                }
            }

            
        }
    }

}
