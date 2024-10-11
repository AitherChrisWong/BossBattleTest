using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSFXEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFXOneTime(AudioClip clip)
    {
        if(TryGetComponent(out AudioSource audioSource))
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
