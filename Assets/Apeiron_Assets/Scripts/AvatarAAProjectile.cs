using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAAProjectile : MonoBehaviour
{
    public Transform targetPos;
    public float speed;

    Vector3 tempTargetPos2;
    // Start is called before the first frame update
    void Start()
    {
        if (targetPos)
            tempTargetPos2 = targetPos.position;
        else
            Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetPos)
            Destroy(transform.parent.gameObject);

        transform.position = Vector3.Lerp(transform.position, tempTargetPos2, speed);
        if(Vector3.Distance(transform.position, tempTargetPos2) < .1f)
        {
            Destroy(transform.parent.gameObject);
        }

        
    }
}
