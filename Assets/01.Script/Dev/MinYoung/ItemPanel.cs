using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Image selectImage;
    private BuyGoods buyGoods;
    public void Set(Sprite sprite, string name)
    {
        itemImage.sprite = sprite;
        nameText.text = name;
    }

    public void SetBuyGoods(BuyGoods buyGoods,  Sprite sprite, string name, string explain)
    {
        this.buyGoods = buyGoods;
        nameText.text = name;
        itemImage.sprite = sprite;
    }
    public void SetExplaniPanel()
    {

    }
    public void SetImage(Sprite sprite)
    {
        selectImage.color = Color.white;
        itemImage.sprite = sprite;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        selectImage.color = Color.red;
        buyGoods.Tooltip(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectImage.color = Color.white;
        buyGoods.Tooltip(false);
    }

   
}
