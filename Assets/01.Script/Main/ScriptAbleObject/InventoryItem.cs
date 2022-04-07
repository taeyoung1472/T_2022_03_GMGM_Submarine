using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "스크립트 에이블 / 아이템")]
public class InventoryItem : ScriptableObject
{
    public Sprite itemSprite;
    public int stackAbleAmount;
    public int id;
    public int weight;
    public string name;
    public string desc;
}