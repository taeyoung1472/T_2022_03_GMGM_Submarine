using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analysis : MonoBehaviour
{
    [SerializeField] private GameObject analysisPanel;
    [SerializeField] private GameObject suplliesPanel;
    [SerializeField] private Text _productName;
    [SerializeField] private Text _productExplain;
    [SerializeField] private Text _productPrice;
    [SerializeField] private Image _productImage;
    //[SerializeField] private List<DescriptionItemSO> _description;
    [SerializeField] private List<DescriptionItemSO> analysisDatas = new List<DescriptionItemSO>();
    DescriptionItemSO currentViewDescriptionDataSO;

    /*public List<DescriptionItemSO> Description
    {
        get => _description;
        set
        {
            _description = value;
        }
    }*/
    public void OnEnable()
    {
        CheckNext();
    }
    public void Set(DescriptionItemSO[] items)
    {
        analysisDatas.AddRange(items);
    }
    public void Storage()
    {
        ItemSOManager.Instance.ItemDataSO.Add(currentViewDescriptionDataSO);
        CheckNext();
    }
    public void Sell()
    {
        MoneyManager.instance.Money += currentViewDescriptionDataSO._disposalPrice;
        CheckNext();
    }
    public void CheckNext()
    {
        if(analysisDatas.Count <= 0)
        {
            EndAnalysis();
            return;
        }
        DescriptionItemSO descObjectSO = analysisDatas[0];
        _productName.text = descObjectSO._productName;
        _productExplain.text = descObjectSO._productExplain;
        _productPrice.text = $"Ã³ºÐ({descObjectSO._disposalPrice}$)";
        _productImage.sprite = descObjectSO._productPainting;
        currentViewDescriptionDataSO = descObjectSO;
        analysisDatas.RemoveAt(0);
    }
    public void EndAnalysis()
    {
        analysisPanel.SetActive(false);
        suplliesPanel.SetActive(true);
    }
    /*private void Awake()
    {
        foreach (DescriptionItemSO description in _description)
        {
            _productName.text = description._productName;
            _productExplain.text = description._productExplain;
            _productPrice.text = description._disposalPrice.ToString();
            _productImage.sprite = description._productPainting;
        }
    }*/
}