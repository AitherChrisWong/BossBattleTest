using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonEnemy : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject tempEnemy = Instantiate(enemy);
        tempEnemy.transform.position = transform.position;
        tempEnemy.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
