using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    [SerializeField] private PositionDisplayManager positionDisplayManager;
    [SerializeField] private int id;
    public void OnTriggerEnter(Collider other)
    {
        positionDisplayManager.SetPlayerPosition(other.GetComponent<PlayerMove>().ID, id);
    }
}
