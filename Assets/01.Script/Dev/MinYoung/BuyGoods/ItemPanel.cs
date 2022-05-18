using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _selectImage;
    private DescriptionItemSO _itemSo;
    //private BuyGoods _buyGoods;
    int index;
    bool isBuyAble;
   
    bool _isSelect = false;
    public void SetIsSelect(bool isSelect)
    {
        _isSelect = isSelect;
        //if (!_isSelect) _selectImage.color = new Color(1,1,1,0);
    }


    private void Start()
    {
        //button = GetComponent<Button>();

        //button.onClick.AddListener(() => SetClickInfo());
    }

    bool _isClick = false;

    void SetClickInfo()
    {
        _isClick = true;
        //_buyGoods._clickInitButton.raycastTarget = true;
        //_buyGoods.GetComponent<BuyGoods>().IsSelect = true;
    }

    public void SetInfo(DescriptionItemSO itemSo, bool _isBuyAble = false)
    {
        this._itemSo = itemSo;
        //this._buyGoods = buyGoods;
        isBuyAble = _isBuyAble;

        _itemImage.sprite = itemSo._productPainting;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (_isSelect) return;
        _selectImage.color = Color.white;
        //_buyGoods.Tooltip(true, _itemSo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (_isSelect) return;
        _selectImage.color = new Color(1, 1, 1, 0);
        //if (!_isClick)
        //    _buyGoods.Tooltip(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        string buttonString = isBuyAble ? "구매" : "";
        Popup.Instance.DisplayPopup(_itemSo._productName, _itemSo._productExplain, _itemSo._productPainting,
            () => 
            {
                if (MoneyManager.instance.PurchaseCheck(_itemSo._disposalPrice))
                {
                    ItemSOManager.Instance.ItemDataSO.Add(_itemSo);
                }
            }, buttonString);
        //print("눌렸어요!");
    }
}
