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
            print("발사");
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
        nameText.text = "어뢰 : " + curTorpedo.torpedoName;
        descText.text = "설명 : " + curTorpedo.torpedoDesc;
        amoutText.text = "갯수 : " + curTorpedo.amount.ToString();
    }
}
[CreateAssetMenu(menuName ="스크립트 에이블 / 어뢰")]
public class Torpedo : ScriptableObject
{
    public int amount;
    public string torpedoName, torpedoDesc;
}