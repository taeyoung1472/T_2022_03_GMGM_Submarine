using UnityEngine;
using System;
[Serializable]
public class UserData
{
    public UserData(string _name)
    {
        name = _name;
    }
    [SerializeField] private string name = "";
    [SerializeField] private bool isReady = false;
    [SerializeField] private bool isLeader = false;
    public string Name { get { return name; } set { name = value; } }
    public bool IsReady { get { return isReady; } set { isReady = value; } }
    public bool IsLeader { get { return isLeader; } set { isLeader = value; } }
}