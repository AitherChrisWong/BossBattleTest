using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballController : MonoBehaviour
{
    public Transform bossPos;
    public float moveSpeed = 1;

    public GameObject vfxFireballExplode;

    // Start is called before the first frame update
    void Start()
    {
        bossPos = GameObject.Find("Boss_001").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,bossPos.position) > .5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, bossPos.position, moveSpeed);
        }else
        {
            StartDestroy();
        }
        
    }

    public void StartDestroy()
    {
        GameObject tempVFX = Instantiate(vfxFireballExplode, transform.position, transform.rotation);
        Destroy(tempVFX, 3f);
        Destroy(this.gameObject);
    }
}
