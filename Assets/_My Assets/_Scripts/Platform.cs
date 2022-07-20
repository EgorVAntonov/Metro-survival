using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject buildingIcon;

    private void Start()
    {
        canvas.SetActive(false);
        buildingIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            canvas.SetActive(true);
            buildingIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            canvas.SetActive(false);
            buildingIcon.SetActive(false);
        }
    }
}
