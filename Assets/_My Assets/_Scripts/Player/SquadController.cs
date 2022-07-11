using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    //[SerializeField] private Transform[] followPoints;

    private List<Unit> units;
    private int currentIndexInLayer;

    private void Start()
    {
        currentIndexInLayer = 0;
        units = new List<Unit>();
    }

    public bool AlreadyHas(Unit unit)
    {
        return units.Contains(unit);
    }

    public void AddUnit(Unit newUnit)
    {
        SetSortingIndexToNewUnit(newUnit);
        units.Add(newUnit);
        if (transform.childCount >= units.Count)
        {
            newUnit.followingTransform = transform.GetChild(units.Count-1);
            return;
        }
        Transform newFollowPoint = new GameObject("Slot (" + (transform.childCount).ToString() + ")").GetComponent<Transform>();
        newFollowPoint.parent = transform;
        newFollowPoint.transform.localPosition = new Vector2(-1.75f - (1f * transform.childCount-1), 0f);
        newUnit.followingTransform = newFollowPoint;
    }

    private void SetSortingIndexToNewUnit(Unit unit)
    {
        unit.SetIndexInLayer(currentIndexInLayer);
        currentIndexInLayer++;
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
