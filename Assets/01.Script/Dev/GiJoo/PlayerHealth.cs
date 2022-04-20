using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// �ؾ��� ��
    /// �̹� �߻��̸� �߻�üũ �ݺ����� �ʱ�(Clear!)
    /// ���� �����
    /// �߻� ������ ���� �̵� �ӵ�, �۾� �ӵ� ġ�� �Ǹ� ���� ���� ������� �ǵ��ƿ��� �ϱ�(����)
    /// ��ó���� ġ��Ǹ� �̵� �ӵ�, �۾� �ӵ� ���� ���� ������� �ǵ��ƿ��� �ϱ�(����)
    /// ��ó���� 2�� ������ ��ȭ�ϴ� �� ����(��ü)
    /// ���ź� ����(����)
    /// </summary>

    enum WherePos
    {
         arms = 0,
         legs,
         head,
         stomach
    }

    enum WhatPsychosis
    {
        illusion = 0,
        hallucination
    }

    [Header("�÷��̾� �⺻ ����")]
    [SerializeField]
    private GetPlayerScript playerStatusData;//ScriptableObject�� �ҷ��� ������

    private PlayerMove_GiJoo playerMove;
    private GameManager_Gijoo gameManager;

    public float playerHpNow;//�÷��̾��� ���� Hp
    public float playerMentalNow;//�÷��̾��� ���� ���ŷ�
    public float playerSpeedNow;//�÷��̾��� ���� �̵� �ӵ�
    public float playerRunningSpeedNow;
    public float playerHandlingNow;//�÷��̾��� ���� �۾� �ӵ�

    private float playerHpTimer;//�÷��̾ �󸶳� �ܻ��� ���� �Ծ����� üũ
    private int daySinceWoundInfection = 0;//��ó ������ ���۵� ��¥
    private int playerBleedingCount;//�÷��̾��� �������� ����
    private int illusionCount;//ȯ�� ����
    private int hallucinationCount;//ȯû ����
    private int woundInfectionCount;//��ó ���� ����
    private float playerInfectionPercent = 100f;//�÷��̾��� ��ó ���� Ȯ��

    private bool isMinored = false;//����ΰ�? true üũ
    private bool isSerioused = false;//�߻��ΰ�? true üũ
    private bool isWoundInfection = false;//��ó������ ���۵Ǿ��°�? true üũ

    

    private void Awake()
    {
        playerHpNow = playerStatusData.PMHp;//�÷��̾��� ���� Hp�� Data ���� ����� �ִ� Hp�� �ʱ�ȭ
        playerMentalNow = playerStatusData.PMMp;//�÷��̾��� ���� ���ŷ��� Data ���� ����� �ִ� ���ŷ����� �ʱ�ȭ
        playerSpeedNow = playerStatusData.PSpd;//�÷��̾��� ���� �̵� �ӵ��� Data ���� ����� �̵��ӵ��� �ʱ�ȭ
        playerRunningSpeedNow = playerStatusData.PRSp;//�÷��̾��� ���� �޸��� �ӵ��� Data ���� ����� �޸���ӵ��� �ʱ�ȭ
        playerHandlingNow = playerStatusData.PMHS;//�÷��̾��� ���� �۾� �ӵ��� Data ���� ����� �̵��ӵ��� �ʱ�ȭ
        playerMove = GetComponent<PlayerMove_GiJoo>();
        gameManager = FindObjectOfType<GameManager_Gijoo>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))//����� ����� �ڵ�
        {
            Damaged(10);
            MentalDamaged(10);
        }
    }


    public void Damaged(float damage) //������� �Ծ��� ��
    {
        playerHpNow -= damage; //Hp�� ���� �������ŭ ����
        if(Random.Range(0,100) >= 95)//���� ������ 5% Ȯ��
        {
            Disease(); // ���� ����
        }
        HpCheck(playerHpNow); //���� Hp�� ���� Hp�� üũ��
    }

    public void Disease() // ����
    {
        Debug.Log("�Ѹ� ������");
       switch(Random.Range(0, 2))
        {
            case 0:
                Cold(); //����
                break;
            case 1:
                Virus(); //���̷���
                break;
        }
    }

    public void Cold() // ����
    {
        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //�̵� �ӵ��� 5% ������
        playerRunningSpeedNow -= (playerRunningSpeedNow * 0.05f); //�޸��� �ӵ��� 5% ������
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //�۾� �ӵ��� 5% ������
    }

    public void Virus() //���̷���
    {

    }

    public void HpCheck(float _playerHpNow) //���� HP üũ�ϴ� �ڵ�
    {
        if (_playerHpNow <= playerStatusData.PMHp * 0.5) //�÷��̾��� ���� HP�� �ִ� HP�� ���� ���ϸ� �߻�
        {
            if (!isSerioused)
            {
                isSerioused = true;
                WoundPhysicCheck();//�߻����� ������� üũ
            }
        }
        else if (_playerHpNow <= playerStatusData.PMHp * 0.8) //�÷��̾��� ���� HP�� �ִ� HP�� 80% ���ϸ� ���
        {
            if (!isMinored)
            {
                isMinored = true;
                WoundPhysicCheck();//�߻����� ������� üũ
            }
        }
        else isMinored = isSerioused = false; //����, Player�� ���� HP�� �ִ� HP�� 80%���� ũ�� �߻� ��� �ƴ�
    }

    public void WoundPhysicCheck() //���, �߻� ���� üũ�ϴ� �ڵ�
    {
        if (isSerioused) //�߻��� True���
        {
            Debug.Log("��ó�� �ɰ���... �켱 ġ�Ḧ �޾ƾ߰ھ�.");
            isMinored = false; //����� False�� ��
            StopCoroutine(MinorWoundPhysic());//����� ���� �ڷ�ƾ�� �ߴ���
            SeriousWoundPhysicPos(); //�߻� ���� üũ
            StartCoroutine(SeriousWoundPhysic());//�߻��� ���� �ڷ�ƾ�� ������
        }
        else if (isMinored) //����� True�� �߻��� False���
        {
            Debug.Log("��¦ ���� �� ������, �� ��� ������.");
            StopCoroutine(SeriousWoundPhysic());//�߻��� ���� �ڷ�ƾ�� �ߴ���
            StartCoroutine(MinorWoundPhysic());//����� ���� �ڷ�ƾ�� ������
        }
        else //�� �� False���
        {
            StopAllCoroutines();//��� �ڷ�ƾ�� �ߴ���
            playerHpTimer = 0f;//üũ �ð��� �����ϴ� ���߿� ������ �� �����Ƿ� �ٽ� 0���� �ʱ�ȭ
            playerBleedingCount = 0;//�������� ������ 0�� ��
            playerInfectionPercent = 100f;//��ó ���� Ȯ���� �ٽ� 0%�� ���ƿ�
        }

    }
    
    public IEnumerator MinorWoundPhysic() //���(�ܻ�)
    {
        while (true) //��� �ڷ�ƾ�� �������� ���� ���
        {
            playerHpTimer = 0f; //üũ �ð��� �ٽ� 0���� �ʱ�ȭ��
            while (playerHpTimer <= 120f) //üũ �ð��� 120��, �� 2���� �� ������
            {
                playerHpTimer += Time.deltaTime; //��� ���� �ð��� �����ϰ� üũ �ð��� ������
                yield return null; //1������ ��
            }
            playerInfectionPercent -= 5f; //��ó ���� Ȯ�� 5% ����
            PlayerWoundInfectionRandomCheck(); //���� ��ó ���� Ȯ���� ���� ��ó ������ �ƴ��� �� �ƴ��� Ȯ����
        }
    }
    public IEnumerator SeriousWoundPhysic() //�߻�(�ܻ�)
    {
        while (true) //�߻� �ڷ�ƾ�� �������� ���� ���
        {
            playerHpTimer = 0f; //üũ �ð��� �ٽ� 0���� �ʱ�ȭ��
            while (playerHpTimer <= 60f) //üũ �ð��� 60��, �� 1���� �� ������
            {
                playerHpTimer += Time.deltaTime; //��� ���� �ð��� �����ϰ� üũ �ð��� ������
                yield return null; //1������ ��
            }
            playerBleedingCount += 1; //���� ���� ī��Ʈ�� 1��ŭ ����
            playerInfectionPercent -= 10f; //��ó ���� Ȯ�� 10% ����
            PlayerWoundInfectionRandomCheck(); //���� ��ó ���� Ȯ���� ���� ��ó ������ �ƴ��� �� �ƴ��� Ȯ����
        }
    }

    public void SeriousWoundPhysicPos() //�߻� ��ġ
    {
        int where = Random.Range(0, 4); //Ȯ��, �� �� �����ϰ� 25%

        switch(where)
        {
            case (int)WherePos.arms:
                Debug.Log("��, ���� �� ���� �� ������..?");
                playerHandlingNow -= playerStatusData.PMHS * 0.1f;
                break; // �۾� �ӵ� 10% �پ��� �ϱ�

            case (int)WherePos.legs:
                Debug.Log("�ٸ��� ������ �� ����..");
                playerMove.isCanRun = false;
                break; // �� �ٰ� �ϱ�

            case (int)WherePos.head:
                Debug.Log("�Ӹ��� ���� ���� ���µ�..?");
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerMove.isCanRun = false;
                break; // �� ����� �� ���� ��

            case (int)WherePos.stomach:
                Debug.Log("��, �谡....");
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerMove.isCanRun = false;
                break; // �� ����� �� ���� ��

            default: break;
        }

    }

    public void PlayerWoundInfectionRandomCheck() //��ó ���� üũ �ڵ�
    {
        if (!isWoundInfection)
        {
            if (Random.Range(0, 100) >= playerInfectionPercent) //0���� 99�� �� �߿� �ϳ��� �������� ����, �� ���� Ȯ������ Ŭ �� ��ó ������ �߻���
            {
                woundInfectionCount = 1; //��ó ���� 1�ܰ� �߻�
                isWoundInfection = true;
            }
        }
    }

    public void WoundInfectionDayCount()
    {
        if(gameManager.dayCount - daySinceWoundInfection == 2 && daySinceWoundInfection != 0)// ��ó ���� �߻� ���� 2���� ������
        {
            woundInfectionCount += 1; //��ó ���� 1�ܰ� ����
            switch(woundInfectionCount)
            {
                case 1:
                    WoundInfection1Step();
                    break;
                case 2:
                    WoundInfection2Step();
                    break;
                case 3:
                    WoundNecrosis();
                    break;
                default:
                    break;
            }
        }
    }

    public void WoundInfection1Step() //��ó���� 1�ܰ�, ���� �� �ð����� 2���� ������ 2�ܰ�� �Ѿ�� ������ ��
    {
        daySinceWoundInfection = gameManager.dayCount;

        Debug.Log("��ó�� ���� ��ȭ�ǰ��ֽ��ϴ�");

        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //�̵� �ӵ��� 5% ������
        playerRunningSpeedNow -= (playerRunningSpeedNow * 0.05f); //�޸��� �ӵ��� 5% ������
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //�۾� �ӵ��� 5% ������
    }

    public void WoundInfection2Step() //��ó���� 2�ܰ�, ���� �� �ð����� 2���� ������ ���� �ܰ�� �Ѿ�� ������ ��
    {
        daySinceWoundInfection = gameManager.dayCount;

        Debug.Log("��ó�� ���� ���� ���� �����߽��ϴ�.");

        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //�̵� �ӵ��� 5% �߰��� ������
        playerRunningSpeedNow -= (playerRunningSpeedNow * 0.05f); //�޸��� �ӵ��� 5% �߰��� ������
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //�۾� �ӵ��� 5% �߰��� ������
    }

    public void WoundNecrosis() //��ó ����
    {
        daySinceWoundInfection = 0;
        woundInfectionCount = 0;
        isWoundInfection = false;

        Debug.Log("��ó�� ��� ���巯�����ϴ�.");

        playerStatusData.PSpd -= playerStatusData.PSpd * 0.1f; //Data ���� �̵��ӵ��� ���������� 10% ������
        playerStatusData.PRSp -= playerRunningSpeedNow * 0.1f; //Data ���� �޸���ӵ��� ���������� 10% ������
        playerStatusData.PMHS -= playerStatusData.PMHS * 0.1f; //Data ���� �۾��ӵ��� ���������� 10% ������

    }

    public void PlayerDiedbyExcessiveBleedimg() //���������� ����
    {
        if(playerBleedingCount >= 15) //�� 1�и��� 1�� �����ϴ� ���� ���� ī��Ʈ�� 15�� ���� ��, �� 15���� ������ �� ������
        {
            Debug.Log("����� ���������� ����߽��ϴ�.");
            SceneManager.LoadScene(0); //�ϴ� ���� �����ϱ� ���� LoadScene �س���
        }
    }
    public void MentalDamaged(float damage) //���ŷ¿� ������� �Ծ��� ��
    {
        playerMentalNow -= damage; //���ŷ��� ���� �������ŭ ����
        MentalCheck(playerMentalNow); //���� ���ŷ��� ���� ���ŷ��� üũ��
    }
    public void MentalCheck(float _playerMentalNow) //���� ���ŷ� üũ�ϴ� �ڵ�
    {
        if (_playerMentalNow <= playerStatusData.PMMp * 0.05) //���� ���ŷ��� 5% ��ŭ ���Ҵٸ�
        {
            illusionCount = 3;
            hallucinationCount = 3;
            playerSpeedNow -= (playerStatusData.PSpd * 0.2f);
            playerRunningSpeedNow -= (playerStatusData.PRSp * 0.2f);
            playerHandlingNow -= (playerStatusData.PMHS * 0.2f);
            Debug.Log("���� �ر�"); //���� �ر� �����̻��� �Ͼ
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.25f) //���� ���ŷ��� 25% ��ŭ ���Ҵٸ�
        {
            if (Random.Range(0, 100) <= 74) //75% Ȯ���� ���ź��� �ɸ�
            {
                Whatpsychosis();
                Debug.Log("'���ŷ� ���� ���, ���� ���� ġ�ᰡ �ʿ��� �����Դϴ�'");
            }
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.5f) //���� ���ŷ��� 50% ��ŭ ���Ҵٸ�
        {
            if (Random.Range(0, 100) <= 49) //50% Ȯ���� ���ź��� �ɸ�
            {
                Whatpsychosis();
                Debug.Log("'���ŷ� ���赵 : 2�ܰ�, ġ�Ḧ �ǰ��մϴ�'");
            }
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.75f) //���� ���ŷ��� 75% ��ŭ ���Ҵٸ�
        {
            if (Random.Range(0, 100) <= 24) //25% Ȯ���� ���ź��� �ɸ�
            {
                Whatpsychosis();
                Debug.Log("'���ŷ� ���赵 : 1�ܰ�'");
            }
        }
        else //���ŷ��� 75%���� ũ�ٸ�
        {
            Debug.Log("���� ����"); //���� ������
        }
    }

    public void Whatpsychosis() //� ���ź��� ����°�
    {
        int whatPsychosis = Random.Range(0, 2);
        switch(whatPsychosis)
        {
            case (int)WhatPsychosis.illusion: //ȯ�� ���ź��̸�
                if (illusionCount < 3) //ȯ�� �ִ� ��ø 3�ܰ� ����
                {
                    Debug.Log("ȯ���� �߻��߽��ϴ�.");
                    illusionCount += 1; //ȯ�� �ܰ谡 1 ������
                    IfIllusion(); //ȯ�� ȿ�� �ڵ� ����
                }
                break;
            case (int)WhatPsychosis.hallucination: //ȯû ���ź��̸�
                if (hallucinationCount < 3) //ȯû �ִ� ��ø 3�ܰ� ����
                {
                    Debug.Log("ȯû�� �߻��߽��ϴ�.");
                    hallucinationCount += 1; //ȯû �ܰ谡 1 ������
                    IfHallucination(); //ȯû ȿ�� �ڵ� ����
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// �� ������ �ϴ� ����
    /// </summary>
    public void IfIllusion()//���⼭ ȯ�� �ٷ�
    {
        switch(illusionCount)//ȯ�� ī��Ʈ�� n�� �� n��ø ȿ���� ����
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    public void IfHallucination()//���⼭ ȯû �ٷ�
    {
        switch (hallucinationCount)//ȯû ī��Ʈ�� n�� �� n��ø ȿ���� ����
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

}
