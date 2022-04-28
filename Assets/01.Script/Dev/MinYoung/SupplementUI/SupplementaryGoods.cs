using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplementaryGoods: MonoBehaviour
{
    [SerializeField] private List<DescriptionItemSO> _description;
    [SerializeField] private Image productImage;
    [SerializeField] private Text productName;
    [SerializeField] private GameObject panel,content;

    public List<DescriptionItemSO> Description
    {
        get => _description;
        set
        {
            _description = value;

        }
    }
    private void Awake()
    {
        int i = 0;
        foreach (DescriptionItemSO description in _description)
        {
            GameObject obj = Instantiate(panel, content.transform);
            obj.SetActive(true);
            obj.GetComponent<SupplementPanel>().Set(_description[i]);
            i++;
        }
    }
}
