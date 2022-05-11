using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storeroom : MonoBehaviour
{
    [SerializeField] BuyGoods _buyGoods;

    List<DescriptionItemSO> _descriptionItemSOlist;

    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _pfStoreroom;

    [SerializeField] private GameObject _purchasePanel;
    [SerializeField] private GameObject _buyPanel;
    private void Awake()
    {
        _descriptionItemSOlist = _buyGoods.GetComponent<BuyGoods>().Description;
    }

   public void Print()
    {
        for (int index = 0; index < _descriptionItemSOlist.Count; index++)
        {
            if (_descriptionItemSOlist[index]._productCount > 0)
            {
                GameObject obj = Instantiate(_pfStoreroom, _parent);
                Debug.Log(obj);

            }
        }


    }

    public void OnGoShop()
    {
        this.gameObject.SetActive(false);
        _buyPanel.gameObject.SetActive(false);
        _purchasePanel.gameObject.SetActive(true);
    }
    public void OnDeep()
    {
        this.gameObject.SetActive(false);
    }
    public void OnStorageOrganization()
    {
        this.gameObject.SetActive(false);
    }
}
