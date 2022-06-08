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
    float mrSpeed;
    public static DamageManager instance;
    [SerializeField] PlayerMove playerMove;
    float move;
    [SerializeField] List<Room> rooms = new List<Room>();

    public Action<int, int> OnHit;
    private void Awake()
    {
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
        Debug.Log($"���ǵ�     :{move}");
        Debug.Log($"�ʴ� ħ��  :{flood}");
        Debug.Log($"ħ��       :{submergence}");

        timer += Time.deltaTime;
        if (timer > 1)
        {
            submergence += flood;
            timer = 0;
        }

    }
    public void FloodHole(int id, int hp)
    {
        Mathf.Clamp(hp, 0, 1000);
        Mathf.Clamp(submergence, 0, 1000);
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
            Debug.Log("��ȣ �ۿ� �Ұ���");
        }
        else if (80 <= submergence && submergence < 100)
        {
            move = mrSpeed * 4 / 10;
            Debug.Log("��ȣ �ۿ� �Ұ���");
        }
        else if (submergence == 100)
        {
            move = mrSpeed * 4 / 10;
            //������� ���� ��� ������ �Ұ���
            Debug.Log("�� ���� ����");
            Debug.Log("��ȣ �ۿ� �Ұ���");
        }


    }

}
