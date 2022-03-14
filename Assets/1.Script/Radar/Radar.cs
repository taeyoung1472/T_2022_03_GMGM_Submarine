using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private List<RadarTarget> targets;
    [SerializeField] private Transform trans, radar, radarZero, radarEnd;
    [SerializeField] private float speed;
    [SerializeField] private RadarStat state;
    [SerializeField] private GameObject virtualTargetTamplate;
    List<GameObject> virtualTarget;
    private Vector2 radarCatchPos;
    private Vector2 catchPos;
    [SerializeField] private float angle;
    bool isX, isY;
    public void AddTarget(RadarTarget rtgt)
    {
        targets.Add(rtgt);
        virtualTarget.Add(Instantiate(virtualTargetTamplate, transform));
    }
    public void Update()
    {
        radar.Rotate(Vector3.up * Time.deltaTime * speed);
        catchPos = new Vector2(angle - 20, angle + 20);
        switch (state)
        {
            case RadarStat.XZ:
                angle = Mathf.Atan2(radarEnd.position.z - radarZero.position.z, radarEnd.position.x - radarZero.position.x) * Mathf.Rad2Deg;
                foreach (RadarTarget tgt in targets)
                {
                    //float tgtAngle = Mathf.Atan2(tgt.position.z, tgt.position.x) * Mathf.Rad2Deg;
                    //if (catchPos.x < tgtAngle && catchPos.y > tgtAngle)
                    //{
                        trans.localPosition = new Vector3(tgt.transform.position.x, 2f, tgt.transform.position.z) * 0.1f;
                    //}
                }
                break;
            case RadarStat.YZ:
                angle = Mathf.Atan2(radarEnd.position.y - radarZero.position.y, radarEnd.position.z - radarZero.position.z) * Mathf.Rad2Deg;
                foreach (RadarTarget tgt in targets)
                {
                    //float tgtAngle = Mathf.Atan2(tgt.position.y, tgt.position.z) * Mathf.Rad2Deg;
                    //if (catchPos.x < tgtAngle && catchPos.y > tgtAngle)
                    //{
                        trans.localPosition = new Vector3(-tgt.transform.position.z, 2f, tgt.transform.position.y) * 0.1f;
                    //}
                }
                break;
            case RadarStat.XY:
                angle = Mathf.Atan2(radarEnd.position.y - radarZero.position.y, radarEnd.position.x - radarZero.position.x) * Mathf.Rad2Deg;
                foreach (RadarTarget tgt in targets)
                {
                    //float tgtAngle = Mathf.Atan2(tgt.position.y, tgt.position.x) * Mathf.Rad2Deg;
                    //if (catchPos.x < tgtAngle && catchPos.y > tgtAngle)
                    //{
                        trans.localPosition = new Vector3(-tgt.transform.position.x, 2f, tgt.transform.position.y) * 0.1f;
                    //}
                }
                break;
        }
    }
}
public enum RadarStat
{
    XZ,
    YZ,
    XY
}