using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDoodAutoPlayAnimation : MonoBehaviour
{
    public string animName;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play(animName, -1, Random.Range(0, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
