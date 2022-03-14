using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTarget : MonoBehaviour
{
    [SerializeField] private Sprite radarSprite;
    [SerializeField] private float size;
    public void Start()
    {
        GameManager.Instance.RadarManager.AddTarget(this);
    }
}
