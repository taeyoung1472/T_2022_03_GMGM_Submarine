using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryGrid> grids;
    [Header("테스트")]
    [SerializeField] private InventoryDisplay[] gridObject;
    [SerializeField] private InventoryItem testItem;
    [SerializeField] private int testAmount;
    #region Debug_ContextMenu
    [ContextMenu("이름설정")]
    public void SetName()
    {
        for (int i = 0; i < gridObject.Length; i++)
        {
            gridObject[i].name = "Display_" + i;
        }
    }
    [ContextMenu("아이템 획득 테스트")]
    public void TestAdd()
    {
        AddItem(testItem, testAmount);
    }
    [ContextMenu("List 초기화")]
    public void Clear()
    {
        grids.Clear();
    }
    [ContextMenu("List 정렬")]
    public void Sort()
    {
        grids = grids.OrderBy(x => x.curItem.id).ThenByDescending(x => x.amount).ToList(); 
        DisplayInventory();
    }
    #endregion
    [ContextMenu("인벤토리 UI 디스플레이하기")]
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
                        int temp = grid.amount - grid.curItem.stackAbleAmount;//temp1 에다가 넘친값을 저장
                        grid.amount = grid.curItem.stackAbleAmount;//amount 에다가 한계치만큼 저장
                        amount = temp;//amount 에다가 넘친값 저장
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
            if (newGrid.amount > newGrid.curItem.stackAbleAmount)//만약 amount가 한계치를 넘을시
            {
                int temp = newGrid.amount - newGrid.curItem.stackAbleAmount;//temp1 에다가 넘친값을 저장
                newGrid.amount = newGrid.curItem.stackAbleAmount;//amount 에다가 한계치만큼 저장
                amount = temp;//amount 에다가 넘친값 저장
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