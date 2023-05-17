using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject _object;

    private void Start()
    {
        _object.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _object.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _object.SetActive(false);
    }
}
