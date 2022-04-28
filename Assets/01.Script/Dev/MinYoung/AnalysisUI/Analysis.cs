using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analysis : MonoBehaviour
{
  [SerializeField] private Text _productName;
  [SerializeField] private Text _productExplain;
  [SerializeField] private Text _productPrice;
  [SerializeField] private Image _productImage;
  [SerializeField] private List< DescriptionItemSO> _description;

    public List<DescriptionItemSO>  Description 
    {
        get => _description;
        set
        {
            _description = value;

        }
    }
    
    private void Awake()
    {
        foreach (DescriptionItemSO description in _description)
        {
            _productName.text = description._productName;
            _productExplain.text = description._productExplain;
            _productPrice.text = description._disposalPrice.ToString();
            _productImage.sprite = description._productPainting;
        }
       
    }
}
