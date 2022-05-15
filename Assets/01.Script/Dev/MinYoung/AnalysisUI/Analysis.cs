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
    public void Start()
    {
        CheckNext();
    }
    public void Storage()
    {
        print($"�����߽��ϴ�! �̸� : {currentViewDescriptionDataSO._productName}");
        CheckNext();
    }
    public void Sell()
    {
        print($"�Ⱦҽ��ϴ�! ���� : {currentViewDescriptionDataSO._disposalPrice}");
        CheckNext();
    }
    public void CheckNext()
    {
        if(analysisDatas.Count <= 0)
        {
            EndAnalysis();
            return;
        }
        print("���� �м��ڷ� �ҷ�����");
        DescriptionItemSO descObjectSO = analysisDatas[0];
        _productName.text = descObjectSO._productName;
        _productExplain.text = descObjectSO._productExplain;
        _productPrice.text = $"ó��({descObjectSO._disposalPrice}$)";
        _productImage.sprite = descObjectSO._productPainting;
        currentViewDescriptionDataSO = descObjectSO;
        analysisDatas.RemoveAt(0);
    }
    public void EndAnalysis()
    {
        print("�м� ����!");
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