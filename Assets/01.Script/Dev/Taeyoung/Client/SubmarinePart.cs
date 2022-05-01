using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubmarinePart : MonoBehaviour
{
    [SerializeField] protected SubmarinePartData partData;
    protected float hp;
    public SubmarinePartData PartData { get { return partData; } }
    public float HP { get { return hp; } set { hp = value; } }
    public abstract void Repair();
}
