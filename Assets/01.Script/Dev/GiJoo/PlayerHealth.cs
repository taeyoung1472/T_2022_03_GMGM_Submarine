using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// �ؾ��� ��
    /// ��ó���� ġ��Ǹ� �̵� �ӵ�, �۾� �ӵ� ���� ���� ������� �ǵ��ƿ��� �ϱ�
    /// �̹� �߻��̸� �߻�üũ �ݺ����� �ʱ�
    /// �߻� ������ ���� �̵� �ӵ�, �۾� �ӵ� ġ�� �Ǹ� ���� ���� ������� �ǵ��ƿ��� �ϱ�
    /// ��ó���� 2�� ������ ��ȭ�ϴ� �� ����
    /// ���ź� ����
    /// </summary>

    enum WherePos
    {
         arms = 0,
         legs,
         head,
         stomach
    }

    [Header("�÷��̾� �⺻ ����")]
    [SerializeField]
    private GetPlayerScript playerStatusData;//ScriptableObject�� �ҷ��� ������

    public float playerHpNow;//�÷��̾��� ���� Hp
    public float playerMentalNow;//�÷��̾��� ���� ���ŷ�
    public float playerSpeedNow;//�÷��̾��� ���� �̵� �ӵ�
    public float playerHandlingNow;//�÷��̾��� ���� �۾� �ӵ�

    private float playerHpTimer;//�÷��̾ �󸶳� �ܻ��� ���� �Ծ����� üũ
    private float playerBleedingCount;//�÷��̾��� �������� ����
    private float playerInfectionPercent = 100f;//�÷��̾��� ��ó ���� Ȯ��

    private bool isMinored = false;//����ΰ�? true üũ
    private bool isSerioused = false;//�߻��ΰ�? true üũ

    

    private void Awake()
    {
        playerHpNow = playerStatusData.PMHp;//�÷��̾��� ���� Hp�� Data ���� ����� �ִ� Hp�� �ʱ�ȭ
        playerMentalNow = playerStatusData.PMMp;//�÷��̾��� ���� ���ŷ��� Data ���� ����� �ִ� ���ŷ����� �ʱ�ȭ
        playerSpeedNow = playerStatusData.PSpd;//�÷��̾��� ���� �̵� �ӵ��� Data ���� ����� �̵��ӵ��� �ʱ�ȭ
        playerHandlingNow = playerStatusData.PMHS;//�÷��̾��� ���� �۾� �ӵ��� ������ ���� ����� �̵��ӵ��� �ʱ�ȭ
    }


    public void Damaged(float damage) //������� �Ծ��� ��
    {
        playerHpNow -= damage; //Hp�� ���� �������ŭ ����
        HpCheck(playerHpNow); //���� Hp�� ���� Hp�� üũ��
        WoundPhysicCheck();//�߻����� ������� üũ
    }

    public void HpCheck(float _playerHpNow) //���� HP üũ�ϴ� �ڵ�
    {
        if (_playerHpNow <= playerStatusData.PMHp * 0.5) //�÷��̾��� ���� HP�� �ִ� HP�� ���� ���ϸ� �߻�
        {
            isSerioused = true;
        }
        else if (_playerHpNow <= playerStatusData.PMHp * 0.8) //�÷��̾��� ���� HP�� �ִ� HP�� 80% ���ϸ� ���
        {
            isMinored = true;
        }
        else isMinored = isSerioused = false; //����, Player�� ���� HP�� �ִ� HP�� 80%���� ũ�� �߻� ��� �ƴ�
    }

    public void WoundPhysicCheck() //���, �߻� ���� üũ�ϴ� �ڵ�
    {
        if (isSerioused) //�߻��� True���
        {
            isMinored = false; //����� False�� ��
            StopCoroutine(MinorWoundPhysic());//����� ���� �ڷ�ƾ�� �ߴ���
            SeriousWoundPhysicPos(); //�߻� ���� üũ
            StartCoroutine(SeriousWoundPhysic());//�߻��� ���� �ڷ�ƾ�� ������
        }
        else if (isMinored) //����� True�� �߻��� False���
        {
            StopCoroutine(SeriousWoundPhysic());//�߻��� ���� �ڷ�ƾ�� �ߴ���
            StartCoroutine(MinorWoundPhysic());//����� ���� �ڷ�ƾ�� ������
        }
        else //�� �� False���
        {
            StopAllCoroutines();//��� �ڷ�ƾ�� �ߴ���
            playerHpTimer = 0f;//üũ �ð��� �����ϴ� ���߿� ������ �� �����Ƿ� �ٽ� 0���� �ʱ�ȭ
            playerBleedingCount = 0f;//�������� ������ 0�� ��
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
        float where = Random.Range(0, 3); //Ȯ��, �� �� �����ϰ� 25%

        switch(where)
        {
            case (float)WherePos.arms:
                playerHandlingNow -= playerStatusData.PMHS * 0.1f;
                break; // �۾� �ӵ� 10% �پ��� �ϱ�

            case (float)WherePos.legs:
                playerSpeedNow -= playerStatusData.PSpd * 0.05f;
                break; // �� �ٰ� �ϱ�, �ٵ� ���� �ٴ� �ڵ� ���� �ȵ����ϱ� �̼� ������ �ϱ�

            case (float)WherePos.head:
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerSpeedNow -= playerStatusData.PSpd * 0.05f;
                break; // �� ����� �� ���� ��

            case (float)WherePos.stomach:
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerSpeedNow -= playerStatusData.PSpd * 0.05f;
                break; // �� ����� �� ���� ��

            default: break;
        }

    }

    public void PlayerWoundInfectionRandomCheck() //��ó ���� üũ �ڵ�
    {
        if (Random.Range(0, 99) >= playerInfectionPercent) //0���� 99�� �� �߿� �ϳ��� �������� ����, �� ���� Ȯ������ Ŭ �� ��ó ������ �߻���
        {
            WoundInfection1Step(); //1�ܰ��� ��ó ���� �߻�
        }
    }

    public void WoundInfection1Step() //��ó���� 1�ܰ�, ���� �� �ð����� 2���� ������ 2�ܰ�� �Ѿ�� ������ ��
    {
        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //�̵� �ӵ��� 5% ������
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //�۾� �ӵ��� 5% ������
    }

    public void WoundInfection2Step() //��ó���� 2�ܰ�, ���� �� �ð����� 2���� ������ ���� �ܰ�� �Ѿ�� ������ ��
    {
        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //�̵� �ӵ��� 5% �߰��� ������
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //�۾� �ӵ��� 5% �߰��� ������
    }

    public void WoundNecrosis() //��ó ����
    {
        playerStatusData.PSpd -= playerStatusData.PSpd * 0.1f; //Data ���� �̵��ӵ��� ���������� 10% ������
        playerStatusData.PMHS -= playerStatusData.PMHS * 0.1f; //Data ���� �۾��ӵ��� ���������� 10% ������
    }

    public void PlayerDiedbyExcessiveBleedimg() //���������� ����
    {
        if(playerBleedingCount >= 15) //�� 1�и��� 1�� �����ϴ� ���� ���� ī��Ʈ�� 15�� ���� ��, �� 15���� ������ �� ������
        {
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
            Debug.Log("���� �ر�"); //���� �ر� �����̻��� �Ͼ
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.25f) //���� ���ŷ��� 25% ��ŭ ���Ҵٸ�
        {
            if (Random.Range(0, 99) <= 74) //75% Ȯ���� 3�ܰ� ���ź��� �ɸ�
                Debug.Log("3�ܰ� ���ź�");
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.5f) //���� ���ŷ��� 50% ��ŭ ���Ҵٸ�
        {
            if (Random.Range(0, 99) <= 49) //50% Ȯ���� 3�ܰ� ���ź��� �ɸ�
                Debug.Log("2�ܰ� ���ź�");
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.75f) //���� ���ŷ��� 75% ��ŭ ���Ҵٸ�
        {
            if(Random.Range(0,99) <= 24) //25% Ȯ���� 3�ܰ� ���ź��� �ɸ�
                Debug.Log("1�ܰ� ���ź�");
        }
        else //���ŷ��� 75%���� ũ�ٸ�
        {
            Debug.Log("���� ����"); //���� ������
        }
    }

}
