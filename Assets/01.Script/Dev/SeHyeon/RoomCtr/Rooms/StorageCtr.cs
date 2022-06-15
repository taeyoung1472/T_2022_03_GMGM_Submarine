using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCtr : Room
{
    
    StorageCtr storageCtr;
    protected override void Start()
    {
        base.Start();
        storageCtr = GetComponent<StorageCtr>();
    }
    private void Update()
    {
        SuriBuwi();
        DamageManager.Instance.FloodHole(storageCtr.id, storageCtr.hp);
        Debug.Log($"���� HP : {storageCtr.id} {storageCtr.hp}");
        Debug.Log($"������ �̸� :{suri.name}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{storageCtr.id}: �浹��");
            storageCtr.hp -= storageCtr.damageValue;
        }
    }

    protected override void SuriBuwi()
    {
        GameObject g = transform.GetChild(0).gameObject; // 0��° �ڽ��� �������
        if (g.CompareTag("Wall"))
        {
            float minZ = transform.localScale.x / 2 - transform.position.z;
            float maxZ = transform.localScale.x / 2 + transform.position.z;
            float minY = transform.localScale.y / 2 - transform.position.y;
            float maxY = transform.localScale.y / 2 + transform.position.y;

            float x = transform.position.x;
            float z = Random.Range(minZ,maxZ);
            float y=Random.Range(minY,maxY);
            Instantiate(suri,new Vector3(x, y, z), Quaternion.identity);
        }
        else if (g.CompareTag("Floor"))
        {

        }


    }
}
