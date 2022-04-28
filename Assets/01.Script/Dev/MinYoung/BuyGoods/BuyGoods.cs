using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGoods : MonoBehaviour
{
    [SerializeField] private List<DescriptionItemSO> _description;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject[] _content = new GameObject[3];
    [SerializeField] private GameObject _tooltipPanel;

    [SerializeField] bool _isTooltip = false;   // false ¸é ¾Æ´Ô

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
        int index = 0;
        for (int i = 0; i < _description.Count; i++)
        {
            switch (_description[i].item)
            {
                case DescriptionItemSO.Item.Food:
                    index = 0;
                    break;
                case DescriptionItemSO.Item.medication:
                    index = 1;
                    break;
                case DescriptionItemSO.Item.Tool:
                    index = 2;
                    break;
                case DescriptionItemSO.Item.Analsysis:
                    break;
                default:
                    break;
            }
            GameObject obj = Instantiate(_panel, _content[index].transform);
            obj.GetComponent<ItemPanel>().SetBuyGoods(this, _description[i]);
            obj.SetActive(true);
            index = 0;
        }
        Tooltip(false);
    }

    private void Update()
    {

        if (_isTooltip)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            _tooltipPanel.transform.position = mousePos;
        }

    }

    public void Tooltip(bool isTooltip, DescriptionItemSO itemSo = null)
    {
        if (!isTooltip)
        {
            _tooltipPanel.SetActive(isTooltip);
            return;
        }
        if (isTooltip)
        {
            StartCoroutine(Delay());
        }
        this._isTooltip = isTooltip;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        _tooltipPanel.transform.position = mousePos;
        _tooltipPanel.GetComponent<ToolTipPanel>().Set(itemSo);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        _tooltipPanel.SetActive(_isTooltip);
        yield break;
    }
}
