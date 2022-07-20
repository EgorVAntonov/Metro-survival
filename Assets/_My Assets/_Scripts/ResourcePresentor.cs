using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcePresentor : MonoBehaviour
{
    [SerializeField] private TMP_Text number;

    public void DisplayResourceCount(int value)
    {
        number.text = value.ToString();
    }
}
