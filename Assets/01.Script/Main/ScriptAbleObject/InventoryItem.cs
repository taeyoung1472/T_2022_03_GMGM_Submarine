using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "��ũ��Ʈ ���̺� / ������")]
public class InventoryItem : ScriptableObject
{
    public Sprite itemSprite;
    public int stackAbleAmount;
    public int id;
    public int weight;
    public string name;
    public string desc;
}