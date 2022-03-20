using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private List <RadarTarget> targets;
    [SerializeField] private List <GameObject> virtualTarget;
    [SerializeField] private Transform radar, radarEnd;
    [SerializeField] private float speed, range;
    [SerializeField] private RadarStat state;
    [SerializeField] private GameObject virtualTargetTamplate;
    [SerializeField] private Transform submarine;
    [SerializeField] Vector3 fixVecX, fixVecY;
    [SerializeField] private Vector2 catchPos;
    [SerializeField] float angle;
    int i = 0;
    public void Start()
    {
        switch (state)
        {
            case RadarStat.XZ:
                fixVecX = Vector3.right; fixVecY = Vector3.forward;
                break;
            case RadarStat.YZ:
                fixVecX = Vector3.up; fixVecY = Vector3.forward;
                break;
            case RadarStat.XY:
                fixVecX = Vector3.right; fixVecY = Vector3.up;
                break;
        }
    }
    public void AddTarget(RadarTarget rtgt)
    {
        targets.Add(rtgt);
        GameObject obj = Instantiate(virtualTargetTamplate, transform);
        obj.GetComponent<SpriteRenderer>().sprite = rtgt.RadarSprite;
        virtualTarget.Add(obj);
    }
    public void Update()
    {
        radar.Rotate(Vector3.up * Time.deltaTime * speed);
    }
    public void FixedUpdate()
    {
        angle = Mathf.Atan2(radarEnd.position.z - radar.position.z, radarEnd.position.x - radar.position.x) * Mathf.Rad2Deg;
        catchPos = new Vector2(angle - 20, angle + 20);
        foreach (GameObject item in virtualTarget)
        {
            Vector3 tgtVecX = new Vector3(
                (targets[i].transform.position.x - submarine.position.x) * fixVecX.x,
                (targets[i].transform.position.y - submarine.position.y) * fixVecX.y,
                (targets[i].transform.position.z - submarine.position.z) * fixVecX.z);
            Vector3 tgtVecY = new Vector3(
                (targets[i].transform.position.x - submarine.position.x) * fixVecY.x,
                (targets[i].transform.position.y - submarine.position.y) * fixVecY.y,
                (targets[i].transform.position.z - submarine.position.z) * fixVecY.z);
            Vector2 fixVec = new Vector2(
                tgtVecX.magnitude * Mathf.Sign(tgtVecX.x != 0 ? tgtVecX.x : tgtVecX.y != 0 ? tgtVecX.y : tgtVecX.z),
                tgtVecY.magnitude * Mathf.Sign(tgtVecY.x != 0 ? tgtVecY.x : tgtVecY.y != 0 ? tgtVecY.y : tgtVecY.z));
            float tgtAngle = Mathf.Atan2(fixVec.y, fixVec.x) * Mathf.Rad2Deg;
            if (catchPos.x < tgtAngle && catchPos.y > tgtAngle && tgtVecX.magnitude < range && tgtVecY.magnitude < range)
            {
                item.GetComponent<VirtualTarget>().Find(new Vector3(fixVec.x * 0.08f, 0.21f, fixVec.y * 0.08f));
            }
            i++;
        }
        i = 0;
    }
}
public enum RadarStat
{
    XZ,
    YZ,
    XY
}