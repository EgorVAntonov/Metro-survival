using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Sawmill,
    MetalFactory
}
public class Building : MonoBehaviour
{
    [Header("References")]
    public Transform entrancePoint;
    [SerializeField] protected SquadController squad;

    [Header("Components")]
    [SerializeField] protected Platform platform;
    [SerializeField] protected GameObject house;

    [Header("Options")]
    [SerializeField] protected int buildingCost;
    [SerializeField] protected BuildingType type;

    public bool IsBuilt { get; protected set; }

    public BuildingType GetBuildingType() => type;
}
