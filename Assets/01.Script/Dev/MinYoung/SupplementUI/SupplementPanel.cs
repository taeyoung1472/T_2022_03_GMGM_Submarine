using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplementPanel : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Image _productImage;

    public void Set(DescriptionItemSO desc)
    {
        _nameText.text = desc.name;
        _productImage.sprite = desc._productPainting;
    }
}
