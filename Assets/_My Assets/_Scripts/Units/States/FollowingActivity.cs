using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingActivity : UnitActivity
{
    [SerializeField] private Unit unit;
    public Transform FollowingTransform { set; private get; }

    public override void DoActivity()
    {
        base.DoActivity();
        if (FollowingTransform == null) return;

        if (Vector2.Distance(transform.position, FollowingTransform.position) > 0.15f)
        {
            unit.MoveTowards(FollowingTransform.position);
        }
        else
        {
            unit.SetAnimation("idle");
        }
    }
}
