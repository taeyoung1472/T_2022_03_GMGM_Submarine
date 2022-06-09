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

    public void TakeOutItem()
    {
        recoveryTextObj.SetActive(true);
        CheckPlayer();
    }

    public void TakeInItem()
    {
        recoveryTextObj.SetActive(false);
    }

    public void CheckPlayer()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(objTransform.position, objTransform.forward, Color.red, 100f * Time.deltaTime);
        Physics.Raycast(objTransform.position, objTransform.forward, out hitInfo, distance);
        if (hitInfo.collider.CompareTag("Player"))
        {
            recoveryText.text = "F 치료해주기";
        }
        else
        {
            recoveryText.text = "F 치료하기";
        }
    }
}
