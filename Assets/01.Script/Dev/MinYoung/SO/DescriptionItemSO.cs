using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Product/Item")]
public class DescriptionItemSO : ScriptableObject
{
    public string _productName; // ���� �̸�
    public int _disposalPrice; // ���� ó�� ����
    [TextArea(3, 5)]
    public string _productExplain; // ���� ����
    public Sprite _productPainting; //���� �׸�
    public int _productCount;
    public bool isAnalsiys;
    public Item item;
    public enum Item
    {
        medication = 0,
        Analsysis,
        Tool,
        Food
    }
}
