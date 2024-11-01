using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterChildMeshBatchUpdate : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color teamLight;

    public List<MeshRenderer> meshes = new List<MeshRenderer>();
    public List<SkinnedMeshRenderer> skinnedMeshes = new List<SkinnedMeshRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        DrillChild(transform);

        foreach (MeshRenderer m in meshes)
        {
            if(m.material.shader.name == "Shader Graphs/OutlineShader(original)")
            {
                m.material.SetColor("Color_2a43bda5622f4b61bb77ff509fcb8bc7", teamLight);
            }
        }

        foreach (SkinnedMeshRenderer m2 in skinnedMeshes)
        {
            if (m2.material.shader.name == "Shader Graphs/OutlineShader(original)")
            {
                m2.material.SetColor("Color_2a43bda5622f4b61bb77ff509fcb8bc7", teamLight);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrillChild(Transform target)
    {
        foreach (Transform tempObj in target)
        {
            if(tempObj.gameObject.activeSelf)
            {
                if (tempObj.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer skinnedMesh))
                {
                    skinnedMeshes.Add(skinnedMesh);
                }

                if (tempObj.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
                {
                    meshes.Add(mesh);
                }

                if (tempObj.childCount > 0)
                {
                    DrillChild(tempObj);
                }
            }
            
        }
    }


    public void MeshOutlineLight(bool active)
    {
        int tempBool = Convert.ToInt32(active);

        foreach (MeshRenderer m in meshes)
        {
            if (m.material.shader.name == "Shader Graphs/OutlineShader(original)")
            {
                m.material.SetInt("_isShowOutline", tempBool);
            }
        }

        foreach (SkinnedMeshRenderer m2 in skinnedMeshes)
        {
            if (m2.material.shader.name == "Shader Graphs/OutlineShader(original)")
            {
                m2.material.SetInt("_isShowOutline", tempBool);
            }

        }
    }

    public void MeshChangeLayer(string layerName)
    {
        foreach (MeshRenderer m in meshes)
        {
            m.gameObject.layer = LayerMask.NameToLayer(layerName);
        }

        foreach (SkinnedMeshRenderer m2 in skinnedMeshes)
        {
            m2.gameObject.layer = LayerMask.NameToLayer(layerName);

        }
    }
}
