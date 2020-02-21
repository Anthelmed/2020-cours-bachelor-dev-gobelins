using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//DoTween for the win
public class PressurePlate : MonoBehaviour
{

    [SerializeField] private GameObject door;

    private Vector3 _defaultPosition;

    private void Start()
    {
        _defaultPosition = door.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        door.transform.DOMoveY(_defaultPosition.y - 3f, 0.3f);
    }

    private void OnTriggerExit(Collider other)
    {
        door.transform.DOMoveY(_defaultPosition.y, 0.3f);
    }
}
