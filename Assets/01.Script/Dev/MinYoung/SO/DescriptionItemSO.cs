using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Product/Item")]
public class DescriptionItemSO : ScriptableObject
{
    public string _productName; // 물건 이름
    public int _disposalPrice; // 물건 처분 가격
    [TextArea(3, 5)]
    public string _productExplain; // 물건 설명
    public Sprite _productPainting; //물건 그림
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
