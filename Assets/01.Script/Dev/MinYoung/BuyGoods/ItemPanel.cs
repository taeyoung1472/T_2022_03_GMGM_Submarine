using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _selectImage;
    private DescriptionItemSO _itemSo;
    private BuyGoods _buyGoods;
    int index;

    public void SetBuyGoods(BuyGoods buyGoods, DescriptionItemSO itemSo)
    {
        this._itemSo = itemSo;
        this._buyGoods = buyGoods;

        _itemImage.sprite = itemSo._productPainting;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectImage.color = Color.red;
        _buyGoods.Tooltip(true, _itemSo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selectImage.color = Color.white;
        _buyGoods.Tooltip(false);
    }
}
