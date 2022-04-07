using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TorpedoManager : MonoBehaviour
{
    [SerializeField] private List<Torpedo> torpedoes;
    [SerializeField] private TextMesh nameText, descText, amoutText;
    private Torpedo curTorpedo;
    private bool isCanUseTorpedo;
    int curTorpedoesId;
    public void Start()
    {
        curTorpedo = torpedoes[0];
        infoUpdate();
    }
    public void LunchTorpedo()
    {
        if (curTorpedo.amount > 0)
        {
            curTorpedo.amount--;
            print("�߻�");
        }
        infoUpdate();
    }
    public void ChangeTorpedo(bool isNext)
    {
        if (isNext)
        {
            curTorpedoesId++;
            if (curTorpedoesId >= torpedoes.Count)
            {
                curTorpedoesId--;
                return;
            }
        }
        else
        {
            curTorpedoesId--;
            if (curTorpedoesId < 0)
            {
                curTorpedoesId++;
                return;
            }
        }
        curTorpedo = torpedoes[curTorpedoesId];
        infoUpdate();
    }
    public void infoUpdate()
    {
        nameText.text = "��� : " + curTorpedo.torpedoName;
        descText.text = "���� : " + curTorpedo.torpedoDesc;
        amoutText.text = "���� : " + curTorpedo.amount.ToString();
    }
}
[CreateAssetMenu(menuName ="��ũ��Ʈ ���̺� / ���")]
public class Torpedo : ScriptableObject
{
    public int amount;
    public string torpedoName, torpedoDesc;
}