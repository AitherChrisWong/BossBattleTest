using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CradleStoryCharacterJumpEvent : MonoBehaviour
{
    public CradleBubbleGround cradleBubbleGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBubbleWiggle()
    {
        if(cradleBubbleGround != null)
            cradleBubbleGround.SetIsWiggle(true);
    }
}
