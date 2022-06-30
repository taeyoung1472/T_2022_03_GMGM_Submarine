using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class DamageManager : MonoBehaviour
{
    GameObject suri;
    int hp = 0;
    float timer = 0;
    int hole = 0;
    int flood = 0;
    int submergence = 0;
    float mrSpeed;
    public static DamageManager instance;
    [SerializeField] PlayerMove playerMove;
    float move;
    [SerializeField] List<Room> rooms = new List<Room>();

    public Action<int, int> OnHit;
    public static DamageManager Instance
    {
        get => instance;
    }
    private void Awake()
    {
        suri = Resources.Load<GameObject>("Prefabs/Suri");
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        move = playerMove.Speed;
        mrSpeed = move;
        int i = 0;
        foreach (Room room in rooms)
        {
            room.id = i;
            i++;
        }
        OnHit += FloodHole;
    }

    // Update is called once per frame
    void Update()
    {

        //playerMove.Speed = move;
        submergence = Mathf.Clamp(submergence, 0, 100);
        Debug.Log($"스피드     :{move}");
        Debug.Log($"초당 침수  :{flood}");
        Debug.Log($"침수       :{submergence}");

        timer += Time.deltaTime;
        if (timer > 1)
        {
            submergence += flood;
            timer = 0;
        }

    }
   
    public void SuriBuwi(Transform mytrans)
    {
        GameObject g = mytrans.GetChild(Random.Range(0, mytrans.childCount)).gameObject; // 랜덤번째 자식을 갖고오기
        //g.Hole();
        //print(Random.Range(0, mytrans.childCount));
        print(g.tag);
            if (g.CompareTag("Wall"))
            {
                float minZ = g.transform.position.z - g.transform.localScale.x / 2;
                float maxZ = g.transform.position.z + g.transform.localScale.x / 2;
                float minY = g.transform.position.y - g.transform.localScale.y / 2;
                float maxY = g.transform.position.y + g.transform.localScale.y / 2;
                float x = g.transform.position.x;
                float z = Random.Range(minZ, maxZ);
                float y = Random.Range(minY, maxY);
                Instantiate(suri, new Vector3(x, y, z), Quaternion.identity);
            }
            else if (g.CompareTag("Ceiling"))
            {
                float minX = g.transform.position.x - g.transform.localScale.x / 2;
                float maxX = g.transform.position.x + g.transform.localScale.x / 2;
                float minZ = g.transform.position.z - g.transform.localScale.y / 2;
                float maxZ = g.transform.position.z + g.transform.localScale.y / 2;
                float y = g.transform.position.y;

                float z = Random.Range(minZ, maxZ);
                float x = Random.Range(minX, maxX);
                Instantiate(suri, new Vector3(x, y, z), Quaternion.identity);
            }
            else if (g.CompareTag("Floor"))
            {
                float minX = g.transform.position.x - g.transform.localScale.z / 2;
                float maxX = g.transform.position.x + g.transform.localScale.z / 2;
                float minZ = g.transform.position.z - g.transform.localScale.x / 2;
                float maxZ = g.transform.position.z + g.transform.localScale.x / 2;
                float y = g.transform.position.y;

                float z = Random.Range(minZ, maxZ);
                float x = Random.Range(minX, maxX);
                Instantiate(suri, new Vector3(x, -y, z), Quaternion.identity);
            }
            else if (g.CompareTag("Front"))
            {
                float minX = g.transform.position.x - g.transform.localScale.x / 2;
                float maxX = g.transform.position.x + g.transform.localScale.x / 2;
                float minY = g.transform.position.z - g.transform.localScale.y / 2;
                float maxY = g.transform.position.z + g.transform.localScale.y / 2;
                float y = g.transform.position.y;

                float z = Random.Range(minY, maxY);
                float x = Random.Range(minX, maxX);
                Instantiate(suri, new Vector3(x, y, z), Quaternion.identity);
            }
            else if (g.CompareTag("Back"))
            {
                float minX = g.transform.position.x - g.transform.localScale.x / 2;
                float maxX = g.transform.position.x + g.transform.localScale.x / 2;
                float minY = g.transform.position.y - g.transform.localScale.y / 2;
                float maxY = g.transform.position.y + g.transform.localScale.y / 2;
                float y = g.transform.position.y;

                float z = Random.Range(minY, maxY);
                float x = Random.Range(minX, maxX);
                Instantiate(suri, new Vector3(x, y, -z), Quaternion.identity);
            }
           
        
        
    }
    public void PartHole(int hp)
    {

        hp = Mathf.Clamp(hp, 0, 1000);
        if (hp < 1000 && hp >= 980)
        {
            
        }
        else if (hp < 980 && hp >= 960)
        {

        }
        else if (hp < 960 && hp >= 940)
        {

        }
        else if (hp < 940 && hp >= 920)
        {

        }
        else if (hp < 920 && hp >= 900)
        {

        }
        else if (hp < 900 && hp >= 880)
        {

        }
        else if (hp < 880 && hp >= 860)
        {

        }
        else if (hp < 860 && hp >= 840)
        {

        }
        else if (hp < 840 && hp >= 820)
        {

        }
        else if (hp < 820 && hp >= 800)
        {

        }
        else if (hp < 800 && hp >= 780)
        {

        }
        else if (hp < 780 && hp >= 760)
        {

        }
        else if (hp < 760 && hp >= 740)
        {

        }
        else if (hp < 740 && hp >= 720)
        {

        }
        else if (hp < 720 && hp >= 700)
        {

        }
        else if (hp < 700 && hp >= 680)
        {

        }
        else if (hp < 680 && hp >= 660)
        {

        }
        else if (hp < 660 && hp >= 640)
        {

        }
        else if (hp < 640 && hp >= 620)
        {

        }
        else if (hp < 620 && hp >= 600)
        {

        }
        else if (hp < 600 && hp >= 580)
        {

        }
        else if (hp < 580 && hp >= 560)
        {

        }
        else if (hp < 560 && hp >= 540)
        {

        }
        else if (hp < 540 && hp >= 520)
        {

        }
        else if (hp < 520 && hp >= 500)
        {

        }
        else if (hp < 500 && hp >= 480)
        {

        }
        else if (hp < 480 && hp >= 460)
        {

        }
        else if (hp < 460 && hp >= 440)
        {

        }
        else if (hp < 440 && hp >= 420)
        {

        }
        else if (hp < 420 && hp >= 400)
        {

        }
        else if (hp < 400 && hp >= 380)
        {

        }
        else if (hp < 380 && hp >= 360)
        {

        }
        else if (hp < 360 && hp >= 340)
        {

        }
        else if (hp < 340 && hp >= 320)
        {

        }
        else if (hp < 320 && hp >= 300)
        {

        }
        else if (hp < 300 && hp >= 280)
        {

        }
        else if (hp < 280 && hp >= 260)
        {

        }
        else if (hp < 260 && hp >= 240)
        {

        }
        else if (hp < 240 && hp >= 220)
        {

        }
        else if (hp < 220 && hp >= 200)
        {

        }
        else if (hp < 200 && hp >= 180)
        {

        }
        else if (hp < 180 && hp >= 160)
        {

        }
        else if (hp < 160 && hp >= 140)
        {

        }
        else if (hp < 140 && hp >= 120)
        {

        }
        else if (hp < 120 && hp >= 100)
        {

        }
        else if (hp < 100 && hp >= 80)
        {

        }
        else if (hp < 80 && hp > 60)
        {

        }
        else if (hp < 60 && hp >= 40)
        {

        }
        else if (hp < 40 && hp >= 20)
        {

        }
        else
        {

        }
    }
    //대충 반환값을 speed로 해서 각 구역에 speed를 따로 반환하게 만들 계획
    public void FloodHole(int id, int hp)
    {

        hp = Mathf.Clamp(hp, 0, 1000);
        if (hp >= 760 && hp < 880)
        {
            hole = 1;
            flood = 1;
        }
        else if (hp >= 640 && hp < 760)
        {
            hole = 2;
            flood = 2;
        }
        else if (hp >= 520 && hp < 640)
        {
            hole = 3;
            flood = 3;
        }
        else if (hp >= 400 && hp < 520)
        {
            hole = 4;
            flood = 4;
        }
        else if (hp >= 280 && hp < 400)
        {
            hole = 5;
            flood = 5;
        }
        else if (hp >= 160 && hp < 280)
        {
            hole = 6;
            flood = 6;
        }
        else if (hp < 160)
        {
            hole = 7;
            flood = 7;
        }

        if (submergence < 10)
        {
            move = mrSpeed;
        }
        else if (10 <= submergence && submergence < 20)
        {
            move = mrSpeed * 9 / 10;
        }
        else if (20 <= submergence && submergence < 30)
        {
            move = mrSpeed * 8 / 10;
        }
        else if (30 <= submergence && submergence < 40)
        {
            move = mrSpeed * 7 / 10;
        }
        else if (40 <= submergence && submergence < 50)
        {
            move = mrSpeed * 6 / 10;
        }
        else if (50 <= submergence && submergence < 80)
        {
            move = mrSpeed * 5 / 10;
            Debug.Log("상호 작용 불가능");
        }
        else if (80 <= submergence && submergence < 100)
        {
            move = mrSpeed * 4 / 10;
            Debug.Log("상호 작용 불가능");
        }
        else if (submergence == 100)
        {
            move = mrSpeed * 4 / 10;
            //잠수복이 없을 경우 숨쉬기 불가능
            Debug.Log("숨 못쉼 컥컥");
            Debug.Log("상호 작용 불가능");
        }


    }

}
