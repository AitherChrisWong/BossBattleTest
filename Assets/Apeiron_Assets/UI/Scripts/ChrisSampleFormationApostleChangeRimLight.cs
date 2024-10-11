using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisSampleFormationApostleChangeRimLight : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color rimColor;
    public float rimPower1;
    public float rimPower2;

    // Start is called before the first frame update
    void Start()
    {
        Material coneMat = GetComponent<SkinnedMeshRenderer>().materials[0];
        GetComponent<SkinnedMeshRenderer>().materials[0] = coneMat;

        GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("Color_1", rimColor);
        GetComponent<SkinnedMeshRenderer>().materials[0].SetFloat("Vector1_1", rimPower1);
        GetComponent<SkinnedMeshRenderer>().materials[0].SetFloat("Vector1_2", rimPower2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
