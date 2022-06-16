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
    
    private float distance = 10f;
    private bool isItemOn = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isItemOn = !isItemOn;
        }
        CheckPlayer();
    }

    public void CheckPlayer()
    {
        recoveryTextObj.SetActive(isItemOn);
        RaycastHit hitInfo;
        if (Physics.Raycast(objTransform.position, objTransform.forward, out hitInfo, distance))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                recoveryText.text = "F 치료해주기";
            }
        }
        else
        {
            recoveryText.text = "F 치료하기";
        }
    }
}
