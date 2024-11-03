using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPreviewCollider : MonoBehaviour
{
    public bool isAimEnemy;

    public List<GameObject> aimTargets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        foreach(GameObject target in aimTargets)
        {
            if (target.TryGetComponent<BossControl>(out BossControl bossControl))
            {
                bossControl.skin.GetChild(0).GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer("Default");
            }

            if (target.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
            {
                apostleMovement.skin.GetChild(0).GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer("Default");
            }
        }

        aimTargets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAimEnemy)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if(other.TryGetComponent<BossControl>(out BossControl bossControl))
                {
                    bossControl.skin.GetChild(0).GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer("No Post");

                    aimTargets.Add(other.gameObject);
                }

                if (other.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
                {
                    apostleMovement.skin.GetChild(0).GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer("No Post");

                    aimTargets.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isAimEnemy)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (other.TryGetComponent<BossControl>(out BossControl bossControl))
                {
                    bossControl.skin.GetChild(0).GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer("Default");

                    aimTargets.Remove(other.gameObject);
                }

                if (other.TryGetComponent<ApostleMovement>(out ApostleMovement apostleMovement))
                {
                    apostleMovement.skin.GetChild(0).GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer("Default");

                    aimTargets.Add(other.gameObject);
                }
            }
        }
    }

}
