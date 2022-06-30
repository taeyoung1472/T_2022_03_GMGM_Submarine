using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosGetter : MonoBehaviour
{
    [SerializeField] Vector3 pos = Vector3.zero;
    [SerializeField] Vector3 size = Vector3.one;
    Vector3 rand;
    [ContextMenu("AAA")]
    public void GetRandomPos()
    {
        Vector3 ancher = transform.position + pos;
        Vector3 randVec = ancher;
        randVec += new Vector3(Random.Range(-0.5f, 0.5f) * size.x, Random.Range(-0.5f, 0.5f) * size.y, Random.Range(-0.5f, 0.5f) * size.z);
        rand = randVec;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + pos, size);
        Gizmos.DrawWireSphere(rand, 0.1f);
    }
#endif
}
