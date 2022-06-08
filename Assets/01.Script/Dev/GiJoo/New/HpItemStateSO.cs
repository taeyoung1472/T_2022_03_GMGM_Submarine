using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "스크립트 에이블/Gijoo/HpItem")]
public class HpItemStateSO : ScriptableObject
{
    [SerializeField]
    public float DirectRecovery;
    [SerializeField]
    public float IndirectRecoveryTime;
    [SerializeField]
    public float CoolDown;

    [SerializeField]
    public float IndirSecond; //몇 초마다 1 퍼센트 씩 회복되냐
    [SerializeField]
    public float NoMachMentalDown; //마취 안 했을 때 정신력 다운


    
}
