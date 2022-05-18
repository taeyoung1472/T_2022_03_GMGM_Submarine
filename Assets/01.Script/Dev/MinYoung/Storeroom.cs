using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storeroom : MonoBehaviour
{
    //[SerializeField] BuyGoods _buyGoods;
    public static Storeroom Instance;
    [SerializeField] private Transform _parent;
    [SerializeField] private ItemPanel _pfStoreroom;

    [SerializeField] private GameObject _purchasePanel;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        //_descriptionItemSOlist = _buyGoods.GetComponent<BuyGoods>().Description;
    }
    public void OnEnable()
    {
        Print();
    }

    public void Print()
    {
        /*for (int index = 0; index < itemDataSO.Count; index++)
        {
            if (itemDataSO[index]._productCount > 0)
            {
                GameObject obj = Instantiate(_pfStoreroom.gameObject, _parent);
            }
        }*/
        DescriptionItemSO[] sortedItemSO = ItemSOManager.Instance.ItemDataSO.OrderBy(x => x._productName).ToArray();
        print($"Default Length : {ItemSOManager.Instance.ItemDataSO.Count}");
        int i = 0;
        print(i);
        print($"Length : {sortedItemSO.Length}");
        foreach (var item in itemList)
        {
            item.SetActive(false);
        }
        foreach (DescriptionItemSO item in sortedItemSO)
        {
            if (itemList.Count <= i)
            {
                itemList.Add(Instantiate(_pfStoreroom.gameObject, _parent));//[i] = Instantiate(_pfStoreroom.gameObject, _parent);
            }
            itemList[i].GetComponent<ItemPanel>().SetInfo(item, false);
            itemList[i].SetActive(true);
            i++;
            print(i);
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
        //this.gameObject.SetActive(false);
    }
    public void OnStorageOrganization()
    {
        //this.gameObject.SetActive(false);
    }
}
