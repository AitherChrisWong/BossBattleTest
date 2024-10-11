using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSearchEnemyRange : MonoBehaviour
{
    public AvatarBasicMovement avatarBasicMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            avatarBasicMovement.targets.Add(other.gameObject);
            avatarBasicMovement.FindNearestTarget();
            //print("enemy found: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject!=null)
            {
                avatarBasicMovement.targets.Remove(other.gameObject);
                
            }
            avatarBasicMovement.FindNearestTarget();

        }
    }
}
