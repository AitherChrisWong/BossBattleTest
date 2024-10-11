using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CradleMemoryFragmentSample : MonoBehaviour
{
    [Header("Spirit Glitch")]
    public bool isPlayStartGlitch;
    public bool isPlayEndGlitch;
    public Material glitchMat;
    public AnimationCurve glitchCurve;

    public GameObject spiritGlitch;

    float startSpiritGlitchTime;
    float endSpiritGlitchTime;
    public float startSpeed = 1;
    public float endSpeed = 1;


    [Header("Camera Movement")]
    public Camera cam;
    public AnimationCurve fovCurve;
    public AnimationCurve camPosZCurve;

    [Header("Memory Fragment Video")]
    [SerializeField]
    VideoPlayer videoPlayer;
    public RawImage videoImage;
    public float videoTime;

    [Header("Audio")]
    public float endSfxOffset = .5f;
    public AudioClip sfxTransitStart;
    public AudioClip sfxTransitEnd;


    void Start()
    {
        endSpiritGlitchTime = 0;

        //default disable video play
        spiritGlitch.SetActive(false);
        videoPlayer.enabled = false;
        videoImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayStartGlitch)
        {
            if (startSpiritGlitchTime < 1)
            {
                glitchMat.SetFloat("_OverallGlitchRate", glitchCurve.Evaluate(1- startSpiritGlitchTime));
                cam.transform.localPosition = new Vector3(0, 1, camPosZCurve.Evaluate(1- startSpiritGlitchTime));
                cam.fieldOfView = fovCurve.Evaluate(1- startSpiritGlitchTime);

                startSpiritGlitchTime += Time.deltaTime * startSpeed;
            }
            else
                isPlayStartGlitch = false;
        }

        if(isPlayEndGlitch)
        {
            if (endSpiritGlitchTime < 1)
            {
                glitchMat.SetFloat("_OverallGlitchRate", glitchCurve.Evaluate(endSpiritGlitchTime));
                cam.transform.localPosition = new Vector3(0, 1, camPosZCurve.Evaluate(endSpiritGlitchTime));
                cam.fieldOfView = fovCurve.Evaluate(endSpiritGlitchTime);

                endSpiritGlitchTime += Time.deltaTime * endSpeed;
            }
            else
                isPlayEndGlitch = false;
        }
        

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(StartPlayMemoryFragmentCounter());
            
        }

    }
    

    public void VideoStart()
    {
        //GetComponent<AudioSource>().PlayOneShot(sfxTransitStart);

        videoImage.gameObject.SetActive(true);
        videoPlayer.enabled = true;
        videoPlayer.Play();

        //check video end time
        videoPlayer = GetComponent<VideoPlayer>();

        videoTime = (float)videoPlayer.length;
        //Invoke("videoEnded", videoTime);
    }

    void videoEnded()
    {
        EndSpiritGlitch();
        videoPlayer.enabled = false;
        videoImage.gameObject.SetActive(false);


        print("video end");
    }

    public void EndSpiritGlitch()
    {
        isPlayEndGlitch = true;
        endSpiritGlitchTime = 0;
    }

    IEnumerator StartPlayMemoryFragmentCounter()
    {
        spiritGlitch.SetActive(true);
        isPlayStartGlitch = true;
        startSpiritGlitchTime = 0;
        GetComponent<AudioSource>().PlayOneShot(sfxTransitStart);

        yield return new WaitForSeconds(1);
        spiritGlitch.SetActive(false);

        VideoStart();

        yield return new WaitForSeconds(videoTime - endSfxOffset);
        GetComponent<AudioSource>().PlayOneShot(sfxTransitEnd);

        yield return new WaitForSeconds(endSfxOffset);
        spiritGlitch.SetActive(true);
        videoEnded();

        yield return new WaitForSeconds(1.0f / endSpeed);
        spiritGlitch.SetActive(false);
    }
}
