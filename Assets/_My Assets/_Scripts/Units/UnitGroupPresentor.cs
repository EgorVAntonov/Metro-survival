using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitGroupPresentor : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _count;

    public void UpdatePresentor(Sprite icon, int count)
    {
        if (count <= 0)
        {
            _icon.gameObject.SetActive(false);
            _count.gameObject.SetActive(false);
            return;
        }
        _icon.gameObject.SetActive(true);
        _count.gameObject.SetActive(true);
        _icon.sprite = icon;
        _count.text = count.ToString();
    }

}
