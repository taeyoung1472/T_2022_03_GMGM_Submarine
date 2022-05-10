using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTarget : MonoBehaviour
{
    [SerializeField] private Sprite radarSprite;
    [SerializeField] private float size;
    public Sprite RadarSprite { get { return radarSprite; } }
    public float Size { get { return size; } }
}
