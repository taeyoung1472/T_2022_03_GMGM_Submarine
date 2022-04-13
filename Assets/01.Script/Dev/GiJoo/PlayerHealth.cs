using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// 해야할 일
    /// 상처감염 치료되면 이동 속도, 작업 속도 감소 스택 원래대로 되돌아오게 하기
    /// 이미 중상이면 중상체크 반복하지 않기
    /// 중상 부위에 따른 이동 속도, 작업 속도 치료 되면 감소 스택 원래대로 되돌아오게 하기
    /// 상처감염 2일 지나면 진화하는 거 구현
    /// 정신병 구현
    /// </summary>

    enum WherePos
    {
         arms = 0,
         legs,
         head,
         stomach
    }

    [Header("플레이어 기본 정보")]
    [SerializeField]
    private GetPlayerScript playerStatusData;//ScriptableObject로 불러온 데이터

    public float playerHpNow;//플레이어의 현재 Hp
    public float playerMentalNow;//플레이어의 현재 정신력
    public float playerSpeedNow;//플레이어의 현재 이동 속도
    public float playerHandlingNow;//플레이어의 현재 작업 속도

    private float playerHpTimer;//플레이어가 얼마나 외상을 오래 입었는지 체크
    private float playerBleedingCount;//플레이어의 과다출혈 스택
    private float playerInfectionPercent = 100f;//플레이어의 상처 감염 확률

    private bool isMinored = false;//경상인가? true 체크
    private bool isSerioused = false;//중상인가? true 체크

    

    private void Awake()
    {
        playerHpNow = playerStatusData.PMHp;//플레이어의 현재 Hp를 Data 내에 저장된 최대 Hp로 초기화
        playerMentalNow = playerStatusData.PMMp;//플레이어의 현재 정신력을 Data 내에 저장된 최대 정신력으로 초기화
        playerSpeedNow = playerStatusData.PSpd;//플레이어의 현재 이동 속도를 Data 내에 저장된 이동속도로 초기화
        playerHandlingNow = playerStatusData.PMHS;//플레이어의 현재 작업 속도를 데이터 내에 저장된 이동속도로 초기화
    }


    public void Damaged(float damage) //대미지를 입었을 때
    {
        playerHpNow -= damage; //Hp가 받은 대미지만큼 깎임
        HpCheck(playerHpNow); //현재 Hp를 토대로 Hp를 체크함
        WoundPhysicCheck();//중상인지 경상인지 체크
    }

    public void HpCheck(float _playerHpNow) //현재 HP 체크하는 코드
    {
        if (_playerHpNow <= playerStatusData.PMHp * 0.5) //플레이어의 현재 HP가 최대 HP의 절반 이하면 중상
        {
            isSerioused = true;
        }
        else if (_playerHpNow <= playerStatusData.PMHp * 0.8) //플레이어의 현재 HP가 최대 HP의 80% 이하면 경상
        {
            isMinored = true;
        }
        else isMinored = isSerioused = false; //만약, Player의 현재 HP가 최대 HP의 80%보다 크면 중상도 경상도 아님
    }

    public void WoundPhysicCheck() //경상, 중상 정도 체크하는 코드
    {
        if (isSerioused) //중상이 True라면
        {
            isMinored = false; //경상이 False가 됨
            StopCoroutine(MinorWoundPhysic());//경상일 때의 코루틴을 중단함
            SeriousWoundPhysicPos(); //중상 부위 체크
            StartCoroutine(SeriousWoundPhysic());//중상일 때의 코루틴을 실행함
        }
        else if (isMinored) //경상이 True고 중상이 False라면
        {
            StopCoroutine(SeriousWoundPhysic());//중상일 때의 코루틴을 중단함
            StartCoroutine(MinorWoundPhysic());//경상을 때의 코루틴을 실행함
        }
        else //둘 다 False라면
        {
            StopAllCoroutines();//모든 코루틴을 중단함
            playerHpTimer = 0f;//체크 시간이 증가하는 도중에 끊겼을 수 있으므로 다시 0으로 초기화
            playerBleedingCount = 0f;//과다출혈 스택이 0이 됨
            playerInfectionPercent = 100f;//상처 감염 확률이 다시 0%로 돌아옴
        }

    }
    
    public IEnumerator MinorWoundPhysic() //경상(외상)
    {
        while (true) //경상 코루틴이 실행중인 동안 계속
        {
            playerHpTimer = 0f; //체크 시간이 다시 0으로 초기화됨
            while (playerHpTimer <= 120f) //체크 시간이 120초, 즉 2분이 될 때까지
            {
                playerHpTimer += Time.deltaTime; //계속 실제 시간과 동일하게 체크 시간이 증가함
                yield return null; //1프레임 쉼
            }
            playerInfectionPercent -= 5f; //상처 감염 확률 5% 증가
            PlayerWoundInfectionRandomCheck(); //현재 상처 감염 확률을 토대로 상처 감염이 됐는지 안 됐는지 확인함
        }
    }
    public IEnumerator SeriousWoundPhysic() //중상(외상)
    {
        while (true) //중상 코루틴이 실행중인 동안 계속
        {
            playerHpTimer = 0f; //체크 시간이 다시 0으로 초기화됨
            while (playerHpTimer <= 60f) //체크 시간이 60초, 즉 1분이 될 때까지
            {
                playerHpTimer += Time.deltaTime; //계속 실제 시간과 동일하게 체크 시간이 증가함
                yield return null; //1프레임 쉼
            }
            playerBleedingCount += 1; //과다 출혈 카운트가 1만큼 증가
            playerInfectionPercent -= 10f; //상처 감염 확률 10% 증가
            PlayerWoundInfectionRandomCheck(); //현재 상처 감염 확률을 토대로 상처 감염이 됐는지 안 됐는지 확인함
        }
    }

    public void SeriousWoundPhysicPos() //중상 위치
    {
        float where = Random.Range(0, 3); //확률, 각 각 동일하게 25%

        switch(where)
        {
            case (float)WherePos.arms:
                playerHandlingNow -= playerStatusData.PMHS * 0.1f;
                break; // 작업 속도 10% 줄어들게 하기

            case (float)WherePos.legs:
                playerSpeedNow -= playerStatusData.PSpd * 0.05f;
                break; // 못 뛰게 하기, 근데 아직 뛰는 코드 구현 안됐으니까 이속 느리게 하기

            case (float)WherePos.head:
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerSpeedNow -= playerStatusData.PSpd * 0.05f;
                break; // 두 디버프 다 적용 됨

            case (float)WherePos.stomach:
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerSpeedNow -= playerStatusData.PSpd * 0.05f;
                break; // 두 디버프 다 적용 됨

            default: break;
        }

    }

    public void PlayerWoundInfectionRandomCheck() //상처 감염 체크 코드
    {
        if (Random.Range(0, 99) >= playerInfectionPercent) //0부터 99의 수 중에 하나를 랜덤으로 고르고, 그 수가 확률보다 클 때 상처 감염이 발생함
        {
            WoundInfection1Step(); //1단계의 상처 감염 발생
        }
    }

    public void WoundInfection1Step() //상처감염 1단계, 게임 내 시간으로 2일이 지나면 2단계로 넘어가게 만들어야 함
    {
        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //이동 속도가 5% 감소함
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //작업 속도가 5% 감소함
    }

    public void WoundInfection2Step() //상처감염 2단계, 게임 내 시간으로 2일이 지나면 괴사 단계로 넘어가게 만들어야 함
    {
        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //이동 속도가 5% 추가로 감소함
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //작업 속도가 5% 추가로 감소함
    }

    public void WoundNecrosis() //상처 괴사
    {
        playerStatusData.PSpd -= playerStatusData.PSpd * 0.1f; //Data 상의 이동속도가 영구적으로 10% 감소함
        playerStatusData.PMHS -= playerStatusData.PMHS * 0.1f; //Data 상의 작업속도가 영구적으로 10% 감소함
    }

    public void PlayerDiedbyExcessiveBleedimg() //과다출혈로 죽음
    {
        if(playerBleedingCount >= 15) //매 1분마다 1씩 증가하는 과다 출혈 카운트가 15가 됐을 때, 즉 15분이 지났을 때 실행함
        {
            SceneManager.LoadScene(0); //일단 죽음 구현하기 전에 LoadScene 해놓음
        }
    }
    public void MentalDamaged(float damage) //정신력에 대미지를 입었을 때
    {
        playerMentalNow -= damage; //정신력이 받은 대미지만큼 깎임
        MentalCheck(playerMentalNow); //현재 정신력을 토대로 정신력을 체크함
    }
    public void MentalCheck(float _playerMentalNow) //현재 정신력 체크하는 코드
    {
        if (_playerMentalNow <= playerStatusData.PMMp * 0.05) //만약 정신력이 5% 만큼 남았다면
        {
            Debug.Log("정신 붕괴"); //정신 붕괴 상태이상이 일어남
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.25f) //만약 정신력이 25% 만큼 남았다면
        {
            if (Random.Range(0, 99) <= 74) //75% 확률로 3단계 정신병에 걸림
                Debug.Log("3단계 정신병");
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.5f) //만약 정신력이 50% 만큼 남았다면
        {
            if (Random.Range(0, 99) <= 49) //50% 확률로 3단계 정신병에 걸림
                Debug.Log("2단계 정신병");
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.75f) //만약 정신력이 75% 만큼 남았다면
        {
            if(Random.Range(0,99) <= 24) //25% 확률로 3단계 정신병에 걸림
                Debug.Log("1단계 정신병");
        }
        else //정신력이 75%보다 크다면
        {
            Debug.Log("정상 상태"); //정상 상태임
        }
    }

}
