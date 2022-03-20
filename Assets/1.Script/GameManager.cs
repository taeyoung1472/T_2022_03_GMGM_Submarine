using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private RadarManager radarManager;
    [SerializeField] private User user;
    public User UserInfo { get { return user; } }
    public RadarManager RadarManager { get { return radarManager; } }
    public GameObject Player { get { return player; } }
    private void Awake()
    {
        Instance = this;
        LoadUser();
        SaveUser();
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    public void GoGame()
    {
        SaveUser();
    }
    public void GoMain()
    {
        SaveUser();
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {

    }
    public void AddScroe(int _score)
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
    [ContextMenu("�����ϱ�")]
    public void SaveUser()
    {
        print("����");
        string jsonData = JsonUtility.ToJson(user, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("�ҷ�����")]
    public void LoadUser()
    {
        print("�ҷ�����");
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        string jsonData = File.ReadAllText(path);
        user = JsonUtility.FromJson<User>(jsonData);
    }
}