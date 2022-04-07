using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryGrid> grids;
    [Header("�׽�Ʈ")]
    [SerializeField] private InventoryDisplay[] gridObject;
    [SerializeField] private InventoryItem testItem;
    [SerializeField] private int testAmount;
    #region Debug_ContextMenu
    [ContextMenu("�̸�����")]
    public void SetName()
    {
        for (int i = 0; i < gridObject.Length; i++)
        {
            gridObject[i].name = "Display_" + i;
        }
    }
    [ContextMenu("������ ȹ�� �׽�Ʈ")]
    public void TestAdd()
    {
        AddItem(testItem, testAmount);
    }
    [ContextMenu("List �ʱ�ȭ")]
    public void Clear()
    {
        grids.Clear();
    }
    [ContextMenu("List ����")]
    public void Sort()
    {
        grids = grids.OrderBy(x => x.curItem.id).ThenByDescending(x => x.amount).ToList(); 
        DisplayInventory();
    }
    #endregion
    [ContextMenu("�κ��丮 UI ���÷����ϱ�")]
    public void DisplayInventory()
    {
        int i = 0;
        foreach (InventoryDisplay item in gridObject)
        {
            try
            {
                item.Set(grids[i].curItem.itemSprite, grids[i].amount, grids[i].curItem.weight);
            }
            catch
            {
                item.Set(null, 0, 0);
            }
            i++;
        }
    }
    public void AddItem(InventoryItem data, int amount)
    {
        if(grids.Count != 0)
        {
            foreach (InventoryGrid grid in grids)
            {
                if (grid.curItem.id == data.id)
                {
                    grid.amount += amount;
                    if (grid.amount > grid.curItem.stackAbleAmount)
                    {
                        int temp = grid.amount - grid.curItem.stackAbleAmount;//temp1 ���ٰ� ��ģ���� ����
                        grid.amount = grid.curItem.stackAbleAmount;//amount ���ٰ� �Ѱ�ġ��ŭ ����
                        amount = temp;//amount ���ٰ� ��ģ�� ����
                    }
                }
            }
        }
        while (true)
        {
            if(grids.Count >= gridObject.Length)
            {
                DisplayInventory();
                return;
            }
            InventoryGrid newGrid = new InventoryGrid();
            newGrid.curItem = data;
            newGrid.amount += amount;
            if (newGrid.amount > newGrid.curItem.stackAbleAmount)//���� amount�� �Ѱ�ġ�� ������
            {
                int temp = newGrid.amount - newGrid.curItem.stackAbleAmount;//temp1 ���ٰ� ��ģ���� ����
                newGrid.amount = newGrid.curItem.stackAbleAmount;//amount ���ٰ� �Ѱ�ġ��ŭ ����
                amount = temp;//amount ���ٰ� ��ģ�� ����
                grids.Add(newGrid);
            }
            else
            {
                grids.Add(newGrid);
                break;
            }
        }
        DisplayInventory();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
[Serializable]
public class InventoryGrid
{
    public InventoryItem curItem;
    public int amount;
    public void AddItem(InventoryItem data)
    {
    }
}