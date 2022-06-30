using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCheck : MonoBehaviour
{
    private Text recoveryText;
    private GameObject recoveryTextObj;
    private RecoveryItemManager recoveryItemManager;
    
    private float distance = 10f;
    private void Start()
    {
        recoveryTextObj = GameObject.Find("InfoNameText");
        recoveryText = recoveryTextObj.GetComponent<Text>();
        recoveryItemManager = GameObject.Find("HealthManager").GetComponent<RecoveryItemManager>();
    }

    private void Update()
    {
        CheckPlayer();
    }

    public void CheckPlayer()
    {
        recoveryTextObj.SetActive(recoveryItemManager.IsItemOn);
        if (recoveryItemManager.IsItemOn)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    recoveryText.text = "F 치료해주기";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hitInfo.collider.GetComponent<PlayerRecovery>().UseItem(recoveryItemManager.NowHpItemSO); 
                    }
                }
            }
            else
            {
                recoveryText.text = "F 치료하기";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log(recoveryItemManager.NowHpItemSO);
                    transform.GetComponent<PlayerRecovery>().UseItem(recoveryItemManager.NowHpItemSO);
                }
            }
        }
    }
}
