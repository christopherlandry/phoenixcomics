using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{

    //actors on either side of the stage. actors will appear, front to back, in the order they are in the list,
    //roughly spread out on the stage evenly
    public List<Actor> stageLeft;
    public List<Actor> stageRight;

    public void EnterActor(Actor a, StagePosition direction)
    {
        var area = (direction == StagePosition.STAGE_LEFT) ? stageLeft : stageRight;
        area.Add(a);
        //RedrawActors();
    }

    public void RedrawActors(List<Actor> area)
    {
        var count = area.Count;
    }
}
