using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTouchReader : MonoBehaviour
{
    private Unit unitTouched;
    private Building buildingTouched;

    public delegate void BackgroundTouched();
    public event BackgroundTouched OnBackgroundTouched;

    public delegate void UnitTouched(Unit unit);
    public event UnitTouched OnUnitTouched;

    public delegate void BuildingTouched(Building building);
    public event BuildingTouched OnBuildingTouched;

    private void Update()
    {
        CheckTouches();
    }

    private void CheckTouches()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        if (Input.GetMouseButtonDown(0) == false) return;

        pointer.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        ResetVariables();
        foreach (var hit in raycastResults)
        {
            Debug.Log(hit.gameObject.name, hit.gameObject);

            if (CheckUITouch(hit)) return;
            if (CheckUnitTouch(hit)) return;
            if (CheckBuildingTouch(hit)) return;
            OnBackgroundTouched?.Invoke();
        }
    }

    private void ResetVariables()
    {
        unitTouched = null;
        buildingTouched = null;
    }

    private bool CheckUITouch(RaycastResult hit)
    {
        return hit.gameObject.layer == LayerMask.NameToLayer("UI");
    }

    private bool CheckUnitTouch(RaycastResult hit)
    {
        unitTouched = hit.gameObject.GetComponent<Unit>();
        if (unitTouched == null) return false;

        OnUnitTouched?.Invoke(unitTouched);
        return true;
        
    }

    private bool CheckBuildingTouch(RaycastResult hit)
    {
        buildingTouched = hit.gameObject.GetComponent<Building>();
        if (buildingTouched == null) return false;

        OnBuildingTouched?.Invoke(buildingTouched);
        return true;
    }

    /*
    private void CheckResults()
    {
        if (uiTouched)
        { 
            Debug.Log("UI is touched"); 
            return; 
        }

        if (unitTouched != null)
        {
            Debug.Log("on unit touched");
            OnUnitTouched?.Invoke(unitTouched);
            return;
        }
        if (buildingTouched != null)
        {
            Debug.Log("on building touched");
            OnBuildingTouched?.Invoke(buildingTouched);
            return;
        }
        else
        {
            Debug.Log("building is null");
        }
        if (backgroundTouched)
        {
            Debug.Log("on background touched");
            OnBackgroundTouched?.Invoke();
        }
    }
    */
}
