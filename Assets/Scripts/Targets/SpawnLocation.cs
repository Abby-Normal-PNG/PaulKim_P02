using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : Creature, ITargetable
{
    public override void Target()
    {
        Debug.Log("Spawn Location has been targeted");
    }
}
