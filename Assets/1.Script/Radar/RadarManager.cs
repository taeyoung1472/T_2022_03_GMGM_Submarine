using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarManager : MonoBehaviour
{
    [SerializeField] private Radar[] radars;
    public void AddTarget(RadarTarget rtgt)
    {
        foreach (Radar item in radars)
        {
            item.AddTarget(rtgt);
        }
    }
}