using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject playerTamplate;
    [SerializeField] private Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();
    [SerializeField] private Transform[] content;
    public void SetPlayerPosition(int playerId, int positionId)
    {
        if(!players.ContainsKey(playerId))
        {
            GameObject obj = Instantiate(playerTamplate);
            players.Add(playerId, obj);
        }
        players[playerId].transform.SetParent(content[positionId]);
    }
}
