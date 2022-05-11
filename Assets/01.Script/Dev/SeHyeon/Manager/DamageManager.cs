using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageManager : MonoBehaviour
{
    float timer = 0;
    int hole = 0;
    int flood = 0;
    int submergence = 0;
    float mrSpeed ;
    public static DamageManager instance;
    PlayerMove playerMove;
    float move;
    [SerializeField] List<Room> rooms = new List<Room>();

    public Action<int, int> OnHit;
    private void Awake()
    {
        playerMove =GameObject.Find("Player").GetComponent<PlayerMove>();
        move = playerMove.speed;
        mrSpeed = move;
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
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
        Debug.Log($"스피드{move}");
        Debug.Log($"초당 침수:{flood}");
        Debug.Log($"침수:{submergence}");

            timer+=Time.deltaTime;
            if(timer>1)
            {
                submergence += flood;
                timer=0;
            }
        
    }
     public void FloodHole(int id, int hp)
    {
        Mathf.Clamp(hp, 0, 1000);
        Mathf.Clamp(submergence,0,1000);
        if (hp >= 700 && hp < 850)
        {
           
            hole = 1;
            flood = 1;
        }
        else if (hp >= 550 && hp < 700)
        {
            hole = 2;
            flood = 2;
        }
        else if (hp >= 500 && hp < 550)
        {
            hole = 3;
            flood = 4;
        }
        else if (hp >= 350 && hp < 500)
        {
            hole = 4;
            flood = 6;
        }
        else if (hp >= 200 && hp < 350)
        {
            hole = 5;
            flood = 8;
        }
        else if (hp >= 50 && hp < 200)
        {
            hole = 6;
            flood = 10;
        }
        else if ( hp < 50)
        {
            hole = 7;
            flood = 12;
        }

        if ( submergence < 10)
        {
           move = mrSpeed;
        }
        else if (10 <= submergence && submergence < 20)
        {
           move = mrSpeed * 9 / 10;
        }
        else if (20 <= submergence && submergence < 30)
        {
            move= mrSpeed * 8 / 10;
        }
        else if (30 <= submergence && submergence < 40)
        {
            move = mrSpeed * 7 / 10;
        }
        else if (40 <= submergence && submergence < 50)
        {
            move= mrSpeed * 6 / 10;
        }
        else if (50 <= submergence && submergence < 80)
        {
            move = mrSpeed * 5 / 10;
            Debug.Log("상호 작용 불가능");
        }
        else if (80 <= submergence && submergence < 100)
        {
            move= mrSpeed * 4 / 10;
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
