using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int movementInfluence;

    public delegate void ChangeButtonState(int delta);
    public event ChangeButtonState OnButtonChangedState;

    private int currentPointerID;

    private void Start()
    {
        currentPointerID = -10;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentPointerID != -10) return;

        currentPointerID = eventData.pointerId;
        OnButtonChangedState?.Invoke(movementInfluence);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentPointerID != eventData.pointerId) return;

        currentPointerID = -10;
        OnButtonChangedState?.Invoke(movementInfluence * -1);
    }
}
