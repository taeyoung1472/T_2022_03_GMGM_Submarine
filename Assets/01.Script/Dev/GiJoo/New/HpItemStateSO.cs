using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "��ũ��Ʈ ���̺�/Gijoo/HpItem")]
public class HpItemStateSO : ScriptableObject
{
    [SerializeField]
    public float DirectRecovery;
    [SerializeField]
    public float IndirectRecoveryTime;
    [SerializeField]
    public float CoolDown;

    [SerializeField]
    public float IndirSecond; //�� �ʸ��� 1 �ۼ�Ʈ �� ȸ���ǳ�
    [SerializeField]
    public float NoMachMentalDown; //���� �� ���� �� ���ŷ� �ٿ�


    
}
