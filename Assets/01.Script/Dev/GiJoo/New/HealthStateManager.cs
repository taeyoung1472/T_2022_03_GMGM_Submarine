using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealthStateManager : MonoBehaviour
{
    [SerializeField] private HealthStateDisplay displayObject;
    [SerializeField] private Transform content;
    [SerializeField] private PlayerStat playerState;
    private Dictionary<string, HealthStateDisplay> displayiedObjects = new Dictionary<string, HealthStateDisplay>();
    public Dictionary<string, HealthStateDisplay> DisplayedObjects { get { return displayiedObjects; } }
    public static HealthStateManager Instance;
    Queue<GameObject> displayQueue = new Queue<GameObject>();
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void Add(PlayerPartType type, HealthStateDataSO data)
    {
        string keyString = $"{type}:{data.stateName}";
        if(displayQueue.Count > 0)
        {
            displayiedObjects[keyString] = displayQueue.Dequeue().GetComponent<HealthStateDisplay>();
        }
        displayiedObjects[keyString] = Instantiate(displayObject, content);
        displayiedObjects[keyString].Set(data, type);
        playerState.MoveSpeedFixValue -= data.moveSpeedFixValue;
        playerState.WorkSpeedFixValue -= data.workSpeedFixValue;
        playerState.CoughChance += data.coughChance;
        playerState.InfectionChance += data.infectionChance;
        Sort();
    }
    public void Sort()
    {
        var sorted = from list in displayiedObjects.Values
                     orderby list.CurrentData.strength ascending
                     select list;
        foreach (var item in sorted)
        {
            item.transform.SetAsFirstSibling();
        }
    }
    public void Remove(PlayerPartType type, HealthStateDataSO data)
    {
        string keyString = $"{type}:{data.stateName}";
        if (displayiedObjects.ContainsKey(keyString))
        {
            displayiedObjects[keyString].gameObject.SetActive(false);
            displayQueue.Enqueue(displayiedObjects[keyString].gameObject);
            displayiedObjects.Remove(keyString);
            playerState.MoveSpeedFixValue += data.moveSpeedFixValue;
            playerState.WorkSpeedFixValue += data.workSpeedFixValue;
            playerState.CoughChance -= data.coughChance;
            playerState.InfectionChance -= data.infectionChance;
        }
    }
    //private Dictionary<DisplayObject>;
    /*class DisplayObject
    {
        PartType partType;
        HealthStateDataSO data;
        public DisplayObject(PartType type, HealthStateDataSO so)
        {
            partType = type;
            data = so;
        }
    }*/
}
