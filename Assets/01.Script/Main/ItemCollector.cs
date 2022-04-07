using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private List<ItemObject> collectAbleObjects;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform submarine;
    [SerializeField] private float radius;
    [SerializeField] private MeshRenderer meshRenderer;
    public void Start()
    {
        StartCoroutine(CheckItemObject());
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Vector4(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(submarine.position, radius);
    }
    public void GetItems()
    {
        foreach (ItemObject item in collectAbleObjects)
        {
            inventory.AddItem(item.Item, item.Amount);
        }
        for (int i = 0; i < collectAbleObjects.Count; i++)
        {
            Destroy(collectAbleObjects[0].gameObject);
        }
    }
    IEnumerator CheckItemObject()
    {
        while (true)
        {
            collectAbleObjects.Clear();
            Collider[] collisions = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach (Collider collider in collisions)
            {
                try
                {
                    ItemObject itemObj = collider.GetComponent<ItemObject>();
                    collectAbleObjects.Add(itemObj);
                }
                catch
                {
                    continue;
                }
            }
            if (collectAbleObjects.Count > 0)
            {
                meshRenderer.material.color = Color.green;
            }
            else
            {
                meshRenderer.material.color = Color.white;
            }
            yield return new WaitForSeconds(1);
        }
    }
}