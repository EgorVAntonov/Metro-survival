using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsQuantity : MonoBehaviour
{
    private Dictionary<ActivityName, int> quantity;

    [SerializeField] private UnitGroupPresentor[] presentors;
    [Header("icons")]
    [SerializeField] private Sprite followUnit;
    [SerializeField] private Sprite harvesterUnit;

    private void Start()
    {
        quantity = new Dictionary<ActivityName, int>();
        quantity.Add(ActivityName.following, 0);
        quantity.Add(ActivityName.woodHarvester, 0);
        UpdatePresentors();
    }

    public void AddUnit(ActivityName state)
    {
        if (quantity.ContainsKey(state) == false)
        {
            quantity.Add(state, 0);
        }
        else
        {
            quantity[state]++;
        }
        UpdatePresentors();
    }

    public void RemoveUnit(ActivityName state)
    {
        if (quantity.ContainsKey(state) == false) return;

        quantity[state]--;
        UpdatePresentors();
    }

    private void UpdatePresentors()
    {
        presentors[0].UpdatePresentor(followUnit, quantity[ActivityName.following]);
        presentors[1].UpdatePresentor(harvesterUnit, quantity[ActivityName.woodHarvester]);
    }

    public void HandleUnitChangeState(ActivityName previousState, ActivityName newState)
    {
        //if state == requaired
        //remmove
        //add
    }

}
