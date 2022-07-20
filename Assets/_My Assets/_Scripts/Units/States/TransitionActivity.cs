using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionActivity : UnitActivity
{
    [SerializeField] private Unit unit;

    public Building currentBuilding;

    public override void DoActivity()
    {
        base.DoActivity();
        if (Vector2.Distance(transform.position, currentBuilding.entrancePoint.position) > 0.1f)
        {
            unit.MoveTowards(currentBuilding.entrancePoint.position);
            return;
        }

        unit.SetActivity(ActivityName.woodHarvester);
    }
}
