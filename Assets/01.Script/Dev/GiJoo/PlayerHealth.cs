using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// �ؾ��� ��
    /// 
    /// ���� ����� (�ϴ� ��) (���� �Ϸ�)
    /// 
    /// 
    /// ���� ����� (������ ����) (���� �Ŀ� �Ϸ�)
    /// �߻� ������ ���� �̵� �ӵ�, �۾� �ӵ� ġ�� �Ǹ� ���� ���� ������� �ǵ��ƿ��� �ϱ�(����)
    /// ��ó���� ġ��Ǹ� �̵� �ӵ�, �۾� �ӵ� ���� ���� ������� �ǵ��ƿ��� �ϱ�(����)
    /// �Ƿ� ����(����)
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

    [Header("��繰")]
    [SerializeField]
    private GameObject vomit;
    [SerializeField]
    private Transform vomitPos;
    [Space(2f)]
    [Header("�÷��̾� �⺻ ����")]
    [SerializeField]
    private GetPlayerScript playerStatusData;//ScriptableObject�� �ҷ��� ������

    private PlayerMove_GiJoo playerMove;
    private GameManager_Gijoo gameManager;

    public float playerHpNow;//�÷��̾��� ���� Hp
    public float playerMentalNow;//�÷��̾��� ���� ���ŷ�
    public float playerSpeedNow;//�÷��̾��� ���� �̵� �ӵ�
    public float playerRunningSpeedNow;//�÷��̾��� ���� �޸��� �ӵ�
    public float playerHandlingNow;//�÷��̾��� ���� �۾� �ӵ�
    public float radius;//���� �ڷ�ƾ���� ��� ���� �ݶ��̴��� ����

    private int daySinceWoundInfection = 0;//��ó ������ ���۵� ��¥
    private int playerBleedingCount;//�÷��̾��� �������� ����
    private int illusionCount;//ȯ�� ����
    private int hallucinationCount;//ȯû ����
    private int woundInfectionCount;//��ó ���� ����
    private float playerInfectionPercent = 100f;//�÷��̾��� ��ó ���� Ȯ��

    private bool isMinored = false;//����ΰ�? true üũ
    private bool isSerioused = false;//�߻��ΰ�? true üũ
    private bool isInnerBox = false;//�����ΰ�? true üũ
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
        if (Input.GetKeyDown(KeyCode.Q))//����� ����� �ڵ�
        {
            Damaged(10);
            MentalDamaged(10);
        }
        if (Input.GetKeyDown(KeyCode.E))//���� ����� �ڵ�
        {
            Disease();
        }
    }
    
    public void ChangeSpeedStatus(float percent)
    {
        playerSpeedNow -= (playerStatusData.PSpd * (percent / 100));//�÷��̾��� ���� �̵� �ӵ�
        playerRunningSpeedNow -= (playerStatusData.PRSp * (percent / 100));//�÷��̾��� ���� �޸��� �ӵ�
        playerHandlingNow -= (playerStatusData.PMHS * (percent / 100));//�÷��̾��� ���� �۾� �ӵ�
    }

    #region �ܻ� �κ�
    public void Damaged(float damage) //������� �Ծ��� ��
    {
        playerHpNow -= damage; //Hp�� ���� �������ŭ ����
        if(Random.Range(0,100) >= 95 && !isInnerBox)//���� ������ 5% Ȯ��
        {
           isInnerBox = true;
           StartCoroutine(InnerBox());
        }
        HpCheck(playerHpNow); //���� Hp�� ���� Hp�� üũ��
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
            StopCoroutine(MinorWoundPhysic()); //�ڷ�ƾ ����
            StopCoroutine(SeriousWoundPhysic()); //�ڷ�ƾ ����
            playerBleedingCount = 0;//�������� ������ 0�� ��
            playerInfectionPercent = 100f;//��ó ���� Ȯ���� �ٽ� 0%�� ���ƿ�
        }

    }
    
    public IEnumerator MinorWoundPhysic() //���(�ܻ�)
    {
        while (true) //��� �ڷ�ƾ�� �������� ���� ���
        {
            yield return new WaitForSeconds(120f);
            playerInfectionPercent -= 5f; //��ó ���� Ȯ�� 5% ����
            PlayerWoundInfectionRandomCheck(); //���� ��ó ���� Ȯ���� ���� ��ó ������ �ƴ��� �� �ƴ��� Ȯ����
        }
    }
    #region �߻�
    public IEnumerator SeriousWoundPhysic() //�߻�(�ܻ�)
    {
        while (true) //�߻� �ڷ�ƾ�� �������� ���� ���
        {   
            yield return new WaitForSeconds(60f);
            playerBleedingCount += 1; //���� ���� ī��Ʈ�� 1��ŭ ����
            playerInfectionPercent -= 10f; //��ó ���� Ȯ�� 10% ����
            PlayerWoundInfectionRandomCheck(); //���� ��ó ���� Ȯ���� ���� ��ó ������ �ƴ��� �� �ƴ��� Ȯ����
            PlayerDiedbyExcessiveBleeding(); //���������� ����ϴ��� üũ
        }
    }

    public void SeriousWoundPhysicPos() //�߻� ��ġ
    {
        int where = Random.Range(0, 4); //Ȯ��, �� �� �����ϰ� 25%

        switch(where)
        {
            case (int)WherePos.arms:
                Debug.Log("�Ⱦ���");
                playerHandlingNow -= playerStatusData.PMHS * 0.1f;
                break; // �۾� �ӵ� 10% �پ��� �ϱ�

            case (int)WherePos.legs:
                Debug.Log("�ٸ�����");
                playerMove.isCanRun = false;
                break; // �� �ٰ� �ϱ�

            case (int)WherePos.head:
                Debug.Log("�Ӹ�����");
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerMove.isCanRun = false;
                break; // �� ����� �� ���� ��

            case (int)WherePos.stomach:
                Debug.Log("�����");
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerMove.isCanRun = false;
                break; // �� ����� �� ���� ��

            default: break;
        }

    }
    public void PlayerDiedbyExcessiveBleeding() //���������� ����
    {
        if (playerBleedingCount >= 15) //�� 1�и��� 1�� �����ϴ� ���� ���� ī��Ʈ�� 15�� ���� ��, �� 15���� ������ �� ������
        {
            Debug.Log("����� ���������� ����߽��ϴ�.");
            SceneManager.LoadScene(0); //�ϴ� ���� �����ϱ� ���� LoadScene �س���
        }
    }
    #endregion
    #endregion
    #region ��ó ����
    public void PlayerWoundInfectionRandomCheck() //��ó ���� üũ �ڵ�
    {
        if (!isWoundInfection)
        {
            if (Random.Range(0, 100) >= playerInfectionPercent) //0���� 99�� �� �߿� �ϳ��� �������� ����, �� ���� Ȯ������ Ŭ �� ��ó ������ �߻���
            {
                woundInfectionCount = 1; //��ó ���� 1�ܰ� �߻�
                WoundInfection1Step();
                isWoundInfection = true;
            }
        }
    }

    public void WoundInfectionDayCount()
    {
        if (gameManager.dayCount - daySinceWoundInfection == 2 && daySinceWoundInfection != 0)// ��ó ���� �߻� ���� 2���� ������
        {
            woundInfectionCount += 1; //��ó ���� 1�ܰ� ����

            switch (woundInfectionCount)
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

        ChangeSpeedStatus(5);
    }

    public void WoundInfection2Step() //��ó���� 2�ܰ�, ���� �� �ð����� 2���� ������ ���� �ܰ�� �Ѿ�� ������ ��
    {
        daySinceWoundInfection = gameManager.dayCount;

        Debug.Log("��ó�� ���� ���� ���� �����߽��ϴ�.");

        ChangeSpeedStatus(5);
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
    #endregion
    #region ���� �κ�
    public IEnumerator InnerBox() //����
    {
        int randomInnerBox = Random.Range(0, 2);
        switch (randomInnerBox)
        {
            case 0: //���� �Ŀ�
                BustGuts();
                break;
            case 1: //������
                Concussion();
                break;
        }
        yield return new WaitForSeconds(900f);
        switch (randomInnerBox)
        {
            case 0: //���� �Ŀ� 2�ܰ�
                SeriousBustGuts();
                break;
            case 1: //������ 2�ܰ�
                SeriousConcussion();
                break;
        }

    }
    public void BustGuts()
    {
        ChangeSpeedStatus(15);
    }
    public void SeriousBustGuts()
    {
        ChangeSpeedStatus(15);
    }
    public void Concussion()
    {
        //ȭ�� �ϱ׷����ߵ�
    }
    public void SeriousConcussion()
    {
        //ȭ�� �� �� �ϱ׷����ߵ�
    }
    #endregion
    #region ���� �κ�
    public void Disease() // ����
    {
        Debug.Log("���� �߻�");
        switch (Random.Range(0, 2))
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
        Debug.Log("�ݷ�");
        ChangeSpeedStatus(5);
        StartCoroutine(Cough()); //��ħ �ڷ�ƾ ����
    }

    IEnumerator Cough() //��ħ �ݷ��ݷ�
    {
        while (true)
        {
            float coughTime = Random.Range(20f, 40f); //20~40�ʸ���
            yield return new WaitForSeconds(coughTime);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, 1 << 15); //���� �ݶ��̴� �����ؼ� �� ������ �ִ� �÷��̾� ���̾ �°ԵǸ�
            if (hitColliders != null) //�浹�� ������
            {
                foreach (var hitCollider in hitColliders) //��� �浹ü�� ����� ������ ��� ����
                {
                    if (hitCollider.gameObject != this.gameObject) //�� �ڷ�ƾ ������ �����״� ����Ǹ� �ȵǴϱ� ����
                    {
                        Debug.Log("���⸦ �Ű���ϴ�.");
                        hitCollider.GetComponent<PlayerHealth>().Cold();
                    }
                }
            }
        }
    }

    public void Virus() //���̷���
    {
        Debug.Log("����");
        ChangeSpeedStatus(5);
        StartCoroutine(Vomit());
    }

    IEnumerator Vomit()
    {
        while (true)
        {
            float vomitTime = Random.Range(20f, 40f); //20~40�ʸ���
            yield return new WaitForSeconds(vomitTime);
            Instantiate(vomit, vomitPos.position, vomitPos.rotation); //�� ����
        }
    }
    #endregion
    #region ���ŷ� �κ�
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
            ChangeSpeedStatus(20);
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
    #region ���ź� �κ�
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
    #endregion
    #endregion

    /// <summary>
    /// �� ������ �ϴ� ����
    /// </summary>
    #region ���ź� �κ�
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
    #endregion

}