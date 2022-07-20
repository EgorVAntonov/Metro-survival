using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float harvestDuration;
    private float timeRemainingToHarvest;

    private void Start()
    {
        timeRemainingToHarvest = harvestDuration;
    }

    public void HarvestingFrom(HarvestingWoodActivity harvester)
    {
        timeRemainingToHarvest -= Time.deltaTime;
        if (timeRemainingToHarvest > 0f) return;

        harvester.AddWood();
        timeRemainingToHarvest = harvestDuration;
    }
}
