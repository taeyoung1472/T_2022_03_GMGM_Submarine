using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "��ũ��Ʈ ���̺� / ������")]
public class ItemObject : MonoBehaviour
{
    [SerializeField] private InventoryItem item;
    [SerializeField] private int amount;
    public InventoryItem Item { get { return item; } }
    public int Amount { get { return amount; } }
} 