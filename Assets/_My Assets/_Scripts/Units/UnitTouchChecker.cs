using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitTouchChecker : MonoBehaviour, IPointerDownHandler
{

    public delegate void UnitTouched();
    public event UnitTouched OnUnitTouched;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnUnitTouched?.Invoke();
    }
}
