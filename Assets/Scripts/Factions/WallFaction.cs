using UnityEngine;

public class WallFaction : Faction
{
    public override void Push(int pushCount, Border direction) { 
        Debug.Log("Cannot be pushed at all");
    }

    public override bool OnPushed(Border direction) { 
        Debug.Log("Cannot be pushed at all lmaooo");
        return false;
    }
}
