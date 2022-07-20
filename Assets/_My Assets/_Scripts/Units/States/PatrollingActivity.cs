using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingActivity : UnitActivity
{
    [SerializeField] private Unit unit;
    public UnitsSpawner spawner;

    [SerializeField] private float patrolingRadius;
    [SerializeField] private float waitingTimerMin;
    [SerializeField] private float waitingTimerMax;

    private Vector2 currentTargetPoint;
    private float patrollingWaitingTimer;

    public override void Setup()
    {
        base.Setup();
        currentTargetPoint = transform.position;
        patrollingWaitingTimer = Random.Range(3f, 6f);
    }

    public override void DoActivity()
    {
        base.DoActivity();
        if (Vector2.Distance(transform.position, currentTargetPoint) > 0.1f)
        {
            unit.MoveTowards(currentTargetPoint);
            return;
        }
        patrollingWaitingTimer -= Time.deltaTime;
        if (patrollingWaitingTimer > 0)
        {
            unit.SetAnimation("idle");
            return;
        }
        SetRandomTargetPatrollingPoint();
    }

    private void SetRandomTargetPatrollingPoint()
    {
        currentTargetPoint = new Vector2(spawner.transform.position.x + Random.Range(-patrolingRadius, patrolingRadius), transform.position.y);
        patrollingWaitingTimer = Random.Range(waitingTimerMin, waitingTimerMax);
    }
}
