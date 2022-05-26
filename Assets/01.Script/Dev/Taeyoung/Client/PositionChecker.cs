using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    [SerializeField] private SubmarinePartType partType;
    [SerializeField] private VirtualSubmarineDisplay virtualSubmarineDisplay;
    private int checkIndex = 0;
    public void OnTriggerEnter(Collider other)
    {
        virtualSubmarineDisplay.ElementDic[partType].ProfileImageList[checkIndex].gameObject.SetActive(true);
        checkIndex++;
    }
    public void OnTriggerExit(Collider other)
    {
        checkIndex--;
        virtualSubmarineDisplay.ElementDic[partType].ProfileImageList[checkIndex].gameObject.SetActive(false);
    }
}
