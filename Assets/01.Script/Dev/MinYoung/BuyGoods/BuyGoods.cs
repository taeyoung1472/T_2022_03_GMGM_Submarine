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
    [SerializeField] private Image _clickInitButton;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private InputField _cnt;
    [SerializeField] private GameObject _storeRoom;
    [SerializeField] bool _isTooltip = false;   // false ï¿½ï¿½ ï¿½Æ´ï¿½
    [SerializeField] private Storeroom storeroom;
    public List<DescriptionItemSO> Description
    {
        get => _description;
        set
        {
            _description = value;
        }
    }
    float _delayTime = 0.2f;

    private bool _isSelect = false;

    public bool IsSelect
    {
        set
        {
            _isSelect = value;

            if (_isSelect)
            {
                SetIsSelect();
            }
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
            obj.GetComponent<ItemPanel>().SetInfo(_description[i], true);
            obj.SetActive(true);
            index = 0;
        }
        Tooltip(false);
    }

    void SetIsSelect()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < _content[i].transform.childCount; j++)
            {
                _content[i].transform.GetChild(j).GetComponent<ItemPanel>().SetIsSelect(true);
            }
        }
        _isSelect = true;
        _clickInitButton.raycastTarget = true;
    }
    public void OnClickInit()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < _content[i].transform.childCount; j++)
            {
                _content[i].transform.GetChild(j).GetComponent<ItemPanel>().SetIsSelect(false);
            }
        }

        Tooltip(false);

        _isSelect = false;
        _clickInitButton.raycastTarget = false;
        Debug.Log("?˜ë°”?”ë“¯");
    }


    private void Update()
    {

        if (_delayTime <= 0) 
        {
            _tooltipPanel.SetActive(_isTooltip);
        }
        else
        {
            _delayTime -= Time.deltaTime;
        }

        if (_isTooltip)
        {
            Vector3 mousePos = Vector3.zero;
            mousePos = Input.mousePosition;
            mousePos.z = 0;
            _tooltipPanel.transform.position = mousePos;
        }

    }

    public void Tooltip(bool isTooltip, DescriptionItemSO itemSo = null)
    {
         this._isTooltip = isTooltip;

        if (!_isTooltip)
        {
            _tooltipPanel.SetActive(_isTooltip);
            return;
        }
        else
        {
            _delayTime = 0.2f;
        }

        if (!_isSelect)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            _tooltipPanel.transform.position = mousePos;
            _tooltipPanel.GetComponent<ToolTipPanel>().Set(itemSo);
            _descriptionItemSO = itemSo;
        }
    }
    DescriptionItemSO _descriptionItemSO = null;
   
    public void OnExitShop()
    {
        _buyPanel.SetActive(false);
        _descriptionItemSO = null;
        _cnt.text = null;
    }
    public void OnGoShop()
    {
        _buyPanel.SetActive(true);
    }

    public void Buy()
    {
        if (_cnt.text == null || int.Parse(_cnt.text) <= 0|| _descriptionItemSO == null) return;

        if (MoneyManager.instance.Money <= _descriptionItemSO._disposalPrice)
        {
            Debug.Log("?ˆì„ ì³?ëª¨ì•„");
            return;
        }

        int amount = int.Parse(_cnt.text);
        MoneyManager.instance.Money -= _descriptionItemSO._disposalPrice * amount;
        Debug.Log(MoneyManager.instance.Money -= _descriptionItemSO._disposalPrice);
        _descriptionItemSO._productCount += amount;
        Debug.Log(amount);

        _descriptionItemSO = null;
        _cnt.text = null;

        this.gameObject.SetActive(false);
        _buyPanel.gameObject.SetActive(false);
        Tooltip(false);
        _storeRoom.gameObject.SetActive(true);
        //Debug.Log("?¬ì¡Œ??);
        storeroom.Print();
        //ë§Œì•½ ?€?¥ê³ ê°€ ê½?ì°¨ìžˆ?„ë•Œ ?€?¥ê³¼ ì²˜ë¦¬ ?”ë©´?¼ë¡œ ?´ë™
    }
}
