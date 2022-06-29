using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField]
    private Transform objTransform;
    [SerializeField]
    private Text recoveryText;
    [SerializeField]
    private GameObject recoveryTextObj;
    [SerializeField]
    private RecoveryItemManager recoveryItemManager;
    
    private float distance = 10f;

    private void Update()
    {
        CheckPlayer();
    }

    public void CheckPlayer()
    {
        recoveryTextObj.SetActive(recoveryItemManager.IsItemOn);
        RaycastHit hitInfo;
        if (Physics.Raycast(objTransform.position, objTransform.forward, out hitInfo, distance))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                recoveryText.text = "F 치료해주기";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    hitInfo.collider.GetComponent<PlayerRecovery>().UseItem(recoveryItemManager.NowHpItemSO); //상대 치료해주는 코드
                    //itemCount--; //아이템 갯수 줄어듦
                }
            }
        }
        else
        {
            recoveryText.text = "F 치료하기";
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(recoveryItemManager.NowHpItemSO);
                objTransform.GetComponent<PlayerRecovery>().UseItem(recoveryItemManager.NowHpItemSO); //치료하는 코드
                //itemCount--; //아이템 갯수 줄어듦
            }
        }
    }
}
