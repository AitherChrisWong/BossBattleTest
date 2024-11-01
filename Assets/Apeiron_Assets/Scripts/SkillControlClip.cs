using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkillControlClip : PlayableAsset,ITimelineClipAsset
{
    [SerializeField]
    public SkillControlBehaviour template = new SkillControlBehaviour();
    public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SkillControlBehaviour>.Create(graph, template);
        return playable;

        //throw new System.NotImplementedException();
    }


}
