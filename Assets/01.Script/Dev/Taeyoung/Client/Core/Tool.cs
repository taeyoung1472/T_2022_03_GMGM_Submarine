using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [SerializeField] private ToolData toolData;
    public ToolData ToolData { get; }
    public abstract void OnHand();
    public abstract void OnBag();
    public abstract void Throw();
    public abstract void Pick();
}
