using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analysis : MonoBehaviour
{
  [SerializeField]  private Text productName;
  [SerializeField]  private Text productExplain;
  [SerializeField]  private Text productPrice;
  [SerializeField]  private Image productImage;
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
            productName.text = description._productName;
            productExplain.text = description._productExplain;
            productPrice.text = description._disposalPrice.ToString();
            productImage.sprite = description._productPainting;
        }
       
    }
}
