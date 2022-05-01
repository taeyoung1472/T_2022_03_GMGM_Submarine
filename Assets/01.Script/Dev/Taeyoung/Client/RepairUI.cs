using UnityEngine;
using UnityEngine.UI;

public class RepairUI : MonoBehaviour
{
    [SerializeField] private Text infoText;
    [SerializeField] private Image fillImage;
    public void Display(SubmarinePartData data, float hp, float repairSpeed)
    {
        float time = (data.maxPartHp / repairSpeed) - (data.maxPartHp + (hp - data.maxPartHp)) / repairSpeed;
        infoText.text = $"{data.name} : {hp:0} / {data.maxPartHp}\n{time:0.0} Sec";
        fillImage.fillAmount = hp / data.maxPartHp;
    }
}
