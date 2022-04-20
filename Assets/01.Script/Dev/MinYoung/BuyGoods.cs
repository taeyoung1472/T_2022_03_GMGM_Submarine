using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGoods : MonoBehaviour
{
    [SerializeField] private List<DescriptionItemSO> _description;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] content = new GameObject[3];
    [SerializeField] private GameObject tooltipPanel;

    // false ¸é ¾Æ´Ô
    [SerializeField] bool isTooltip = false;
    
    public List<DescriptionItemSO> Description
    {
        get  => _description;
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
            GameObject obj = Instantiate(panel, content[index].transform);
            obj.SetActive(true);
            obj.GetComponent<ItemPanel>().SetImage(_description[i]._productPainting);
            obj.GetComponent<ItemPanel>().SetBuyGoods(this.GetComponent<BuyGoods>(),
                _description[i]._productPainting,
                _description[i]._productName,
                _description[i]._productExplain);
            index = 0;
        }
        Tooltip(false);
    }
    private void Start()
    {

    }
    private void Update()
    {

        if (isTooltip)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            tooltipPanel.transform.position = mousePos;
        }

    }

    public void Tooltip(bool isTooltip)
    {
        if (isTooltip) StartCoroutine(Delay());
        else tooltipPanel.SetActive(isTooltip);
        this.isTooltip = isTooltip;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        tooltipPanel.transform.position = mousePos;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        tooltipPanel.SetActive(isTooltip);
        yield break;
    }




}
