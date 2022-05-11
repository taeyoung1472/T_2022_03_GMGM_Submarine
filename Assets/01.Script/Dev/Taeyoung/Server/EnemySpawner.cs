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
    public void SpawnEnemy(int id, Vector3 pos)
    {
        Instantiate(enemy, pos, Quaternion.identity).GetComponent<Enemy_Network>().Initialize(id);
    }
}
