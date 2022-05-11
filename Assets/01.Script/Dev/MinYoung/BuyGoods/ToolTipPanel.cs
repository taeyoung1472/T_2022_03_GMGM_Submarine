using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPanel : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Image _productImage;
    [SerializeField] private Text _explainText;
    DescriptionItemSO desc;

    public DescriptionItemSO _DescriptionItemSO
    {
        get
        {
            return desc;
        }
    }
    public void Set(DescriptionItemSO desc)
    {
        _nameText.text = desc.name;
        _productImage.sprite = desc._productPainting;
        _explainText.text = desc._productExplain;
    }
}
