using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquadController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UnitsQuantity unitsCounter;
    [SerializeField] private InputTouchReader touchReader;
    [SerializeField] private GameObject arrow;
    
    [Header("Options")]
    [SerializeField] private Vector2 arrowOffset;

    private List<Unit> squad;
    private int currentIndexInLayer;

    private Unit currentSelectedUnit;

    private void OnEnable()
    {
        touchReader.OnBackgroundTouched += CancelSelection;
        touchReader.OnUnitTouched += HandleUnitTouch;
        touchReader.OnBuildingTouched += HandleBuildingTouch;
    }

    private void OnDisable()
    {
        touchReader.OnBackgroundTouched -= CancelSelection;
        touchReader.OnUnitTouched -= HandleUnitTouch;
        touchReader.OnBuildingTouched -= HandleBuildingTouch;
    }

    private void Start()
    {
        currentSelectedUnit = null;
        arrow.SetActive(false);
        currentIndexInLayer = 0;
        squad = new List<Unit>();
    }

    private void AddUnitToSquad(Unit newUnit)
    {
        squad.Add(newUnit);
        unitsCounter.AddUnit(ActivityName.following);
        SetSortingIndexToNewUnit(newUnit);

        if (transform.childCount >= squad.Count)
        {
            newUnit.SetFollowingTransform(transform.GetChild(squad.Count - 1));
            newUnit.SetActivity(ActivityName.following);
            return;
        }

        newUnit.SetFollowingTransform(CreateNewFollowPoint());
        newUnit.SetActivity(ActivityName.following);
    }

    private void SetSortingIndexToNewUnit(Unit unit)
    {
        unit.SetIndexInLayer(currentIndexInLayer);
        currentIndexInLayer++;
    }

    private Transform CreateNewFollowPoint()
    {
        Transform newFollowPoint = new GameObject("Slot (" + (transform.childCount).ToString() + ")").GetComponent<Transform>();
        newFollowPoint.parent = transform;
        newFollowPoint.transform.localPosition = new Vector2(-1.75f - (1f * transform.childCount - 1), 0f);
        return newFollowPoint;
    }

    private void HandleUnitTouch(Unit unit)
    {
        if (squad.Contains(unit))
        {
            SelectUnit(unit);
            return;
        }
        if (unit.CanBeRecruited() == false) return;

        AddUnitToSquad(unit);
    }

    public void SelectUnit(Unit unit)
    {
        if (currentSelectedUnit == unit) return;

        currentSelectedUnit = unit; 
        SetArrow(unit.transform);
    }

    private void SetArrow(Transform unitTransform)
    {
        arrow.SetActive(true);
        arrow.transform.parent = unitTransform;
        arrow.transform.localPosition = arrowOffset;
    }

    private void HandleBuildingTouch(Building building)
    {
        if (currentSelectedUnit == null) return;
        if (building.IsBuilt == false) return;

        if (building.GetBuildingType() == BuildingType.Sawmill)
        {
            HandleSawmillTouch((Sawmill)building);
        }
    }

    private void HandleSawmillTouch(Sawmill sawmill)
    {
        if (currentSelectedUnit == null) return;
        if (sawmill.IsBuilt == false) return;

        currentSelectedUnit.SetCurrentSawmill(sawmill);
        currentSelectedUnit.SetActivity(ActivityName.transitioning);
        squad.Remove(currentSelectedUnit);
        unitsCounter.RemoveUnit(ActivityName.following);
        unitsCounter.AddUnit(ActivityName.woodHarvester);
        ShiftFollowPoints();
        CancelSelection();
    }

    private void ShiftFollowPoints()
    {
        Unit[] unitsArray = squad.ToArray();
        for (int i = 0; i < unitsArray.Length; i++)
        {
            unitsArray[i].SetFollowingTransform(transform.GetChild(i)); 
        }
    }

    private void CancelSelection()
    {
        arrow.transform.parent = transform.parent;
        arrow.SetActive(false);
        currentSelectedUnit = null;
    }

    public void UpdateRelativePosition(int direction)
    {
        if (direction == 0) return;

        if (direction > 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}
