using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Radar : MonoBehaviour
{
    [SerializeField] private Transform radar;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private RadarStat state;
    [SerializeField] private GameObject virtualTargetTamplate;
    [SerializeField] private Transform submarine;
    [SerializeField] Vector3 fixVecX, fixVecY, otherVec;
    [Range(10, 100)] [SerializeField] private int mapMatrixCount = 25;
    [SerializeField] private Transform mapChecker;
    [SerializeField] private LayerMask mapCheckLayer;
    [SerializeField] private Sprite mapCheckSprite;
    [SerializeField] private Transform targetCheckCollider;
    private Queue<GameObject> virtualTarget = new Queue<GameObject>();
    float calculRange;
    public void Start()
    {
        CalculRange();
        switch (state)
        {
            case RadarStat.XZ:
                fixVecX = Vector3.forward; fixVecY = Vector3.right; otherVec = Vector3.up;
                break;
            case RadarStat.YZ:
                fixVecX = Vector3.up; fixVecY = Vector3.forward; otherVec = Vector3.right;
                break;
            case RadarStat.XY:
                fixVecX = Vector3.right; fixVecY = Vector3.up; otherVec = Vector3.forward;
                break;
        }
        StartCoroutine(MapCheckCorutine());
        targetCheckCollider.transform.position = Vector3.zero;
        targetCheckCollider.Rotate(fixVecX, 90);
    }
    public void Update()
    {
        switch (state)
        {
            case RadarStat.XZ:
                radar.localEulerAngles = new Vector3(0, targetCheckCollider.localEulerAngles.z - 90, 0);
                break;
            case RadarStat.YZ:
                radar.localEulerAngles = new Vector3(0, targetCheckCollider.localEulerAngles.z - 180, 0);
                break;
            case RadarStat.XY:
                radar.localEulerAngles = new Vector3(0, targetCheckCollider.localEulerAngles.y, 0);
                break;
        }
        targetCheckCollider.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RadarTarget"))
        {
            Vector3 ditectedPos;
            VirtualTarget vt = DisplayTarget(other.transform.position, out ditectedPos);
            if(vt != null)
            {
                vt.transform.localScale = Vector3.one * other.GetComponent<RadarTarget>().Size;
                vt.Set(ditectedPos, other.gameObject.GetComponent<RadarTarget>().RadarSprite, EnqueueObject);
            }
        }
    }
    public void EnqueueObject(GameObject targetObject)
    {
        targetObject.SetActive(false);
        virtualTarget.Enqueue(targetObject);
    }
    public void CalculRange()
    {
        calculRange = 1f / range;
    }
    IEnumerator MapCheckCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            MapCheck(otherVec);
        }
    }
    public void MapCheck(Vector3 axis)
    {
        float fixAngle = 360f / mapMatrixCount;
        RaycastHit hit;
        for (int i = 0; i < mapMatrixCount; i++)
        {
            Vector3 dir;
            switch (state)
            {
                case RadarStat.XZ:
                    dir = mapChecker.forward;
                    axis = Vector3.up;
                    break;
                default:
                    dir = mapChecker.up;
                    break;
            }
            Debug.DrawRay(mapChecker.position, mapChecker.up, Color.red, 1f);
            if(Physics.Raycast(mapChecker.position, dir, out hit, 10000f, mapCheckLayer))
            {
                Vector3 ditectedPos;
                VirtualTarget vt = DisplayTarget(hit.point, out ditectedPos);
                if (vt != null)
                {
                    vt.transform.localScale = Vector3.one * 0.1f;
                    vt.Set(ditectedPos, mapCheckSprite, EnqueueObject);
                }
            }
            mapChecker.Rotate(axis, fixAngle);
        }
    }
    public VirtualTarget DisplayTarget(Vector3 targetPos, out Vector3 outPos)
    {
        if (virtualTarget.Count <= 0)
            virtualTarget.Enqueue(Instantiate(virtualTargetTamplate, transform));
        Vector3 pivotVec = targetPos;
        Vector3 tgtVecX = new Vector3(pivotVec.x * fixVecX.x, pivotVec.y * fixVecX.y, pivotVec.z * fixVecX.z);
        Vector3 tgtVecY = new Vector3(pivotVec.x * fixVecY.x, pivotVec.y * fixVecY.y, pivotVec.z * fixVecY.z);
        float xPos = tgtVecX.magnitude * Mathf.Sign(tgtVecX.x != 0 ? tgtVecX.x : tgtVecX.y != 0 ? tgtVecX.y : tgtVecX.z);
        float yPos = tgtVecY.magnitude * Mathf.Sign(tgtVecY.x != 0 ? tgtVecY.x : tgtVecY.y != 0 ? tgtVecY.y : tgtVecY.z);
        Vector2 spotPos = new Vector2(-xPos, -yPos) * calculRange;

        if (spotPos.magnitude > 0.75f) { outPos = Vector3.zero; return null; }
        GameObject tgt = virtualTarget.Dequeue();
        tgt.SetActive(true);
        VirtualTarget vTarget = tgt.GetComponent<VirtualTarget>();
        outPos = new Vector3(spotPos.x, 0.15f, spotPos.y);
        return vTarget;
    }
    #region Legarcy Code
    //private List<RadarTarget> targets;
    /*public void AddTarget(RadarTarget rtgt)
    {
    targets.Add(rtgt);
    GameObject obj = Instantiate(virtualTargetTamplate, transform);
    obj.GetComponent<SpriteRenderer>().sprite = rtgt.RadarSprite;
    virtualTarget.Add(obj);
    }*/
    /*private IEnumerator Rading()
    {
    while (true)
    {
        yield return new WaitForSeconds(0.1f);
        angle = Mathf.Atan2(radarEnd.localPosition.z, radarEnd.localPosition.x) * Mathf.Rad2Deg;
        catchPos = new Vector2(angle - 5, angle + 5);
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

            if (catchPos.x < tgtAngle && catchPos.y > tgtAngle)
            {
                Vector2 tempVec = new Vector2(fixVec.x, fixVec.y) * 0.01f;
                if (tempVec.magnitude > 1) { tempVec.Normalize(); }
                item.GetComponent<VirtualTarget>().Set(new Vector3(tempVec.x * 0.9f, 0.15f, tempVec.y * 0.9f));
            }
            i++;
        }
        i = 0;
    }
                    if (virtualTarget.Count <= 0)
                    virtualTarget.Enqueue(Instantiate(virtualTargetTamplate, transform));

                Vector3 pivotVec = hit.point;
                Vector3 tgtVecX = new Vector3(pivotVec.x * fixVecX.x, pivotVec.y * fixVecX.y, pivotVec.z * fixVecX.z);
                Vector3 tgtVecY = new Vector3(pivotVec.x * fixVecY.x, pivotVec.y * fixVecY.y, pivotVec.z * fixVecY.z);
                float xPos = tgtVecX.magnitude * Mathf.Sign(tgtVecX.x != 0 ? tgtVecX.x : tgtVecX.y != 0 ? tgtVecX.y : tgtVecX.z);
                float yPos = tgtVecY.magnitude * Mathf.Sign(tgtVecY.x != 0 ? tgtVecY.x : tgtVecY.y != 0 ? tgtVecY.y : tgtVecY.z);

                Vector2 spotPos = new Vector2(-xPos, -yPos) * calculRange;
                GameObject tgt = virtualTarget.Dequeue();
                tgt.SetActive(true);
                tgt.transform.localScale = Vector3.one * 0.1f;
                VirtualTarget vTarget = tgt.GetComponent<VirtualTarget>();
                if (spotPos.magnitude > 0.75f) { spotPos = spotPos.normalized * 0.75f; }
                vTarget.Set(new Vector3(spotPos.x, 0.15f, spotPos.y), mapCheckSprite, EnqueueObject);
    if (virtualTarget.Count <= 0)
    virtualTarget.Enqueue(Instantiate(virtualTargetTamplate, transform));
Vector3 pivotVec = other.transform.position;
Vector3 tgtVecX = new Vector3(pivotVec.x * fixVecX.x, pivotVec.y * fixVecX.y, pivotVec.z * fixVecX.z);
Vector3 tgtVecY = new Vector3(pivotVec.x * fixVecY.x, pivotVec.y * fixVecY.y, pivotVec.z * fixVecY.z);
float xPos = tgtVecX.magnitude * Mathf.Sign(tgtVecX.x != 0 ? tgtVecX.x : tgtVecX.y != 0 ? tgtVecX.y : tgtVecX.z);
float yPos = tgtVecY.magnitude * Mathf.Sign(tgtVecY.x != 0 ? tgtVecY.x : tgtVecY.y != 0 ? tgtVecY.y : tgtVecY.z);
Vector2 spotPos = new Vector2(-xPos, -yPos) * calculRange;

GameObject tgt = virtualTarget.Dequeue();
tgt.SetActive(true);
tgt.transform.localScale = Vector3.one * other.GetComponent<RadarTarget>().Size;
VirtualTarget vTarget = tgt.GetComponent<VirtualTarget>();
if (spotPos.magnitude > 0.75f) { spotPos = spotPos.normalized * 0.75f; }
vTarget.Set(new Vector3(spotPos.x, 0.15f, spotPos.y), other.GetComponent<RadarTarget>().RadarSprite, EnqueueObject);
}
*/
    #endregion
}
public enum RadarStat
{
    XZ,
    YZ,
    XY
}