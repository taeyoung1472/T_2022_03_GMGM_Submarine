using UnityEngine;

public class StorageCtr : Room
{
    Transform dd;
    StorageCtr storageCtr;
    private void Awake()
    {
        
        dd = GetComponent<Transform>();
        storageCtr = GetComponent<StorageCtr>();
    }
    void Start()
    {
        DamageManager.Instance.SuriBuwi(dd);
    }
    private void Update()
    {
        DamageManager.Instance.PartHole(hp);
        DamageManager.Instance.FloodHole(storageCtr.id, storageCtr.hp);
        print($"지금 HP : {storageCtr.id} {storageCtr.hp}");
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{storageCtr.id}: 충돌함");
            storageCtr.hp -= storageCtr.damageValue;
        }
    }


}
