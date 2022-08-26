using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField] private Enemy_Network enemy;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void SpawnEnemy(int id, Vector3 pos, Quaternion rot)
    {
        Enemy_Network e = Instantiate(enemy, pos, Quaternion.identity).GetComponent<Enemy_Network>();
        Client.Instance.enemies.Add(id, e);
    }
}
