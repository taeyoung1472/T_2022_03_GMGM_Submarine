using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoPanel : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private float zPos;
    void Update()
    {
        cursor.position = Vector2.MoveTowards(cursor.position, Input.mousePosition, 1);
        zPos += Input.GetAxisRaw("Mouse ScrollWheel");
    }
}
