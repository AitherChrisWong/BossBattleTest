using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScreenspaceSpeedline : MonoBehaviour
{
    public Transform bossPos;
    public Material speedlineMaterial;

    public Vector3 screenspacePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossPos)
        {
            
            screenspacePosition = Camera.main.WorldToViewportPoint(bossPos.position);
            speedlineMaterial.SetVector("_offset", new Vector2(screenspacePosition.x - .5f, screenspacePosition.y - .5f));
        }
    }
}
