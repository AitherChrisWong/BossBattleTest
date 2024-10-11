using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionStarSmoothFollow : MonoBehaviour
{
    public Vector3 targetPos;

    public GameObject VFXTrigger;

    Vector3 refV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != targetPos)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref refV, .2f);
        }
    }

    public void UpdateTargetPosition(Vector3 pos)
    {
        targetPos = pos;
        VFXTrigger.SetActive(true);
        VFXTrigger.GetComponent<Animator>().Play("DistributionBarTrigger", -1, 0);
    }
}
