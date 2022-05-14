using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Network : MonoBehaviour
{
    public static GameManager_Network Instance;

    public static Dictionary<int, Player_Network> players = new Dictionary<int, Player_Network>();

    public static Dictionary<int, bool> isPlayersSpawn = new Dictionary<int, bool>();
    public static Dictionary<int, NetworkObject> netObjects = new Dictionary<int, NetworkObject>();
    //public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
    //public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    //public static Dictionary<int, ProjectileManager> projectiles_enemies = new Dictionary<int, ProjectileManager>();
    //public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;
    public GameObject projectilePrefab;
    public GameObject enemyPrefab;
    [SerializeField] private Map_Network map;
    public Map_Network Map { get => map; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("인스턴스는 이미 있습니다, 오브젝트를 삭제합니다!");
            Destroy(this);
        }
    }

    public GameObject SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.Instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }
        _player.GetComponent<Player_Network>().Inintialize(_id, _username);
        players.Add(_id, _player.GetComponent<Player_Network>());
        return _player;
    }

    public void CreateItemSpawner(int _spawnerId, Vector3 _positon, bool _hasItem)
    {
        /*GameObject _spawner = Instantiate(itemSpawnerPrefab, _positon, itemSpawnerPrefab.transform.rotation);
        _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
        itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());*/
    }

    public void SpawnProjectile(int _id, Vector3 _position)
    {
        /*GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());*/
    }
    public void SpawnProjectile_Enemy(int _id, Vector3 _position)
    {
        /*GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles_enemies.Add(_id, _projectile.GetComponent<ProjectileManager>());*/
    }
    public void SpawnEnemy(int _id, Vector3 _position)
    {
        /*GameObject _enemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
        _enemy.GetComponent<EnemyManager>().Initialize(_id);
        enemies.Add(_id, _enemy.GetComponent<EnemyManager>());*/
    }
    public void InitNetworkTransform(int id, string name)
    {
        if (NetworkTransformManager.instance.NetTransforms.ContainsKey(id))
        {
            return;
        }
        NetworkTransform netTrans = GameObject.Find(name).AddComponent<NetworkTransform>();
        netTrans.ID = id;
        NetworkTransformManager.instance.NetTransforms.Add(id, netTrans);//NetTransforms[id] = netTrans;
    }
}
