using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCreater : MonoBehaviour
{
    public Transform[] createPos;
    public GameObject vfxFireball;

    public float curTime = 0;
    public float createTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        curTime = createTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime < 0)
        {
            GameObject tempFireball = Instantiate(vfxFireball);
            tempFireball.transform.position = createPos[Random.Range(0, createPos.Length)].position;
            tempFireball.name = "VFX_Boss_fireball";

            curTime = createTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
}
