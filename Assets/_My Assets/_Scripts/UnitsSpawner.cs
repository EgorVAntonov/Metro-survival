using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform unitsParent;

    [SerializeField] private float spawnPeriod;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int maxFreeUnits;

    private float timer;
    private int currentFreeUnits;

    private void Start()
    {
        timer = spawnPeriod;
        currentFreeUnits = 0;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNewUnit();
        }

        if (currentFreeUnits >= maxFreeUnits) return;
        timer -= Time.deltaTime;
        if (timer > 0) return;
        SpawnNewUnit();
        ResetTimer();
    }

    private void SpawnNewUnit()
    {
        Vector3 newPosition = GetPositionForUnit();

        Unit newUnit = Instantiate(unitPrefab, newPosition, Quaternion.identity, unitsParent).GetComponent<Unit>();
        newUnit.SetSpawner(this);
        currentFreeUnits++;
    }

    private Vector3 GetPositionForUnit()
    {
        return new Vector3(transform.position.x + Random.Range(-spawnRadius, spawnRadius), 0f, 0f);
    }

    private void ResetTimer()
    {
        timer = spawnPeriod;
    }

    public void ReduceFreeUnitCount()
    {
        currentFreeUnits--;
        if (currentFreeUnits < 0) currentFreeUnits = 0;
    }
}
