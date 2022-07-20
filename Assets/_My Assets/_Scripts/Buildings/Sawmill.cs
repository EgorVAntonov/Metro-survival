using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : Building
{
    [SerializeField] private Wallet wallet;
    [SerializeField] private Tree closestTree;

    private void Start()
    {
        IsBuilt = false;
        platform.gameObject.SetActive(true); 
        house.SetActive(false);
    }

    public void TryBuild()
    {
        if (wallet.TrySpendCoins(buildingCost))
        {
            Build();
        }
    }

    private void Build()
    {
        IsBuilt = true;
        platform.gameObject.SetActive(false);
        house.SetActive(true);
    }

    public void HandleTouch()
    {
    //    if (IsBuilt == false) return;

    //    Debug.Log("Sawmill is touched");
    //    squad.HandleSawmillTouch(this);
    }

    public Tree GetClosestTree()
    {
        return closestTree;
    }
}
