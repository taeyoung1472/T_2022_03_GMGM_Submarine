using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOManager : MonoBehaviour
{
    public static ItemSOManager Instance;
    [SerializeField] private List<DescriptionItemSO> itemDataSO = new List<DescriptionItemSO>();
    public List<DescriptionItemSO> ItemDataSO { get { return itemDataSO; } }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
