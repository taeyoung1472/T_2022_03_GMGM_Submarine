using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _selectImage;
    [SerializeField] private Button button;
    private DescriptionItemSO _itemSo;
    private BuyGoods _buyGoods;
    int index;
   
    bool _isSelect = false;
    public void SetIsSelect(bool isSelect)
    {
        _isSelect = isSelect;
        if (!_isSelect) _selectImage.color = Color.white;
    }


    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => SetClickInfo());
    }

    bool _isClick = false;

    void SetClickInfo()
    {
        _isClick = true;
        //_buyGoods._clickInitButton.raycastTarget = true;
        _buyGoods.GetComponent<BuyGoods>().IsSelect = true;
    }

    public void SetBuyGoods(BuyGoods buyGoods, DescriptionItemSO itemSo)
    {
        this._itemSo = itemSo;
        this._buyGoods = buyGoods;

        _itemImage.sprite = itemSo._productPainting;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isSelect) return;
        _selectImage.color = Color.red;
        _buyGoods.Tooltip(true, _itemSo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelect) return;

        _selectImage.color = Color.white;
        if (!_isClick)
            _buyGoods.Tooltip(false);
    }
}
