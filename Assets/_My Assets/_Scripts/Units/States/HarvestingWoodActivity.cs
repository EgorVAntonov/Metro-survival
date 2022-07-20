using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingWoodActivity : UnitActivity
{
    [SerializeField] private Unit unit;
    public Sawmill currentBuilding;
    [SerializeField] private TreeDetector detector;

    [SerializeField] private int maxWoodCarry;

    private Tree currentClosestTree;
    private int currentWoodCarry;

    public override void Setup()
    {
        base.Setup();
        currentWoodCarry = 0;
    }

    public override void DoActivity()
    {
        base.DoActivity();
        if (currentWoodCarry >= maxWoodCarry)
        {
            DeliverWood();
            return;
        }

        currentClosestTree = currentBuilding.GetClosestTree();
        if (currentClosestTree == null)
        {
            detector.ExpandSearchArea();
            return;
        }

        if (Vector2.Distance(transform.position, currentClosestTree.transform.position) > 1.1f)
        {
            unit.MoveTowards(currentClosestTree.transform.position);
            return;
        }

        unit.SetAnimation("attack");
        currentClosestTree.HarvestingFrom(this);
    }

    private void DeliverWood()
    {
        if (Vector2.Distance(transform.position, currentBuilding.entrancePoint.position) > 0.1f)
        {
            unit.MoveTowards(currentBuilding.entrancePoint.position);
            return;
        }

        Wallet.instance.AddWood(maxWoodCarry);
        currentWoodCarry -= maxWoodCarry;
    }

    public void AddWood()
    {
        currentWoodCarry++;
    }
}
