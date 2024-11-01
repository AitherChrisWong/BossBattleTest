using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1,1,1)]
[TrackClipType(typeof(SkillControlClip))]
[TrackBindingType(typeof(AvatarBasicMovement))]


public class SkillControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<SkillControlMixerBehaviour>.Create(graph,inputCount);


    }
}
