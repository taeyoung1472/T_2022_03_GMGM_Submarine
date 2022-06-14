using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TorpedoController : MonoBehaviour
{
    [Header("어뢰 조준 관련")]
    [SerializeField] Transform camHorizontal;
    [SerializeField] Transform camVertical;
    [SerializeField][Range(100,1000)] float sensitivity = 500;
    [Header("어뢰 발사 관련")]
    [SerializeField] Transform torpedoSpawnPos;
    [SerializeField] LocalTorpedo torpedo;
    [Header("락온 관련")]
    Camera cam;
    [SerializeField] List<GameObject> targets = new List<GameObject>();
    [SerializeField] Vector2 lookRange = Vector2.one * 0.1f;
    List<Transform> lookOnTable = new List<Transform>();
    [SerializeField] Image borderUp;
    [SerializeField] Image borderDown;
    [SerializeField] Image borderRight;
    [SerializeField] Image borderLeft;
    [SerializeField] GameObject lookOnMark;
    [SerializeField] float lookOnTime = 2.5f;
    bool isLookOn = false;
    Transform lookObject;
    [Header("사운드")]
    [SerializeField] AudioClip launchClip;
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(TorpedoCycle());
        StartCoroutine(LookOn());
        Transform canvas = FindObjectOfType<Canvas>().transform;
        borderUp.transform.position = cam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f + lookRange.y));
        borderDown.transform.position = cam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f - lookRange.y));
        borderRight.transform.position = cam.ViewportToScreenPoint(new Vector2(0.5f + lookRange.x, 0.5f));
        borderLeft.transform.position = cam.ViewportToScreenPoint(new Vector2(0.5f - lookRange.x, 0.5f));
    }
    void Update()
    {
        CamRotate();
        CheckLookOnObjects();
    }
    IEnumerator TorpedoCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) && isLookOn);
            LaunchTorpedo();
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator LookOn() 
    {
        while (true)
        {
            isLookOn = false;
            yield return new WaitUntil(() => lookObject != null);
            GameObject prevLookObject = lookObject.gameObject;
            float time = 0;
            while (CheckCanLook(prevLookObject.transform))
            {
                time += Time.deltaTime;
                if (time > lookOnTime)
                {
                    isLookOn = true;
                }
                else
                {
                    lookOnMark.transform.localScale = Vector3.one * (time / lookOnTime);
                }
                yield return null;
            }
        }
    }

    private void LaunchTorpedo()
    {
        AudioPoolManager.instance.Play(launchClip, transform.position);
        Instantiate(torpedo, torpedoSpawnPos.position, Quaternion.identity).Set(lookObject);
    }
    private void CheckLookOnObjects()
    {
        foreach (GameObject _target in targets)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(_target.transform.position);
            if(viewPos.x < 0.5f + lookRange.x && viewPos.x > 0.5f - lookRange.x &&
               viewPos.y < 0.5f + lookRange.y && viewPos.y > 0.5f - lookRange.y)
            {
                lookOnTable.Add(_target.transform);
            }
        }
        if (lookOnTable.Count > 1)
        {
            float minDdist = 1024;
            foreach (Transform detectedTrans in lookOnTable)
            {
                Vector3 viewPos = cam.WorldToViewportPoint(detectedTrans.position);
                if (Vector2.Distance(viewPos, new Vector2(0.5f, 0.5f)) < minDdist){
                    lookObject = detectedTrans;
                    minDdist = Vector2.Distance(viewPos, new Vector2(0.5f, 0.5f));
                }
            }
        }
        else if(lookOnTable.Count == 1)
        {
            lookObject = lookOnTable[0];
        }
        else
        {
            lookObject = null;
        }
        if(lookObject != null)
        {
            lookOnMark.SetActive(true);
            lookOnMark.transform.position = cam.ViewportToScreenPoint(cam.WorldToViewportPoint(lookObject.transform.position));
        }
        else
        {
            lookOnMark.SetActive(false);
        }
        lookOnTable.Clear();
    }
    private void CamRotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = -Input.GetAxisRaw("Mouse Y");
        camHorizontal.rotation = camHorizontal.rotation * Quaternion.Euler(0, mouseX * sensitivity * Time.deltaTime, 0);
        Quaternion rot = camVertical.localRotation * Quaternion.Euler(mouseY * sensitivity * Time.deltaTime, 0, 0);
        float verticalRot = rot.eulerAngles.x;
        if (verticalRot < 90)
        {
            verticalRot = Mathf.Clamp(verticalRot, 0, 90);
        }
        else
        {
            verticalRot = Mathf.Clamp(verticalRot, 0, 360);
        }
        rot.eulerAngles = new Vector3(verticalRot, 0, 0);
        camVertical.localRotation = rot;
    }
    bool CheckCanLook(Transform prev)
    {
        if (lookObject == null) return false;
        else if (prev != lookObject) return false;
        return true;
    }
}