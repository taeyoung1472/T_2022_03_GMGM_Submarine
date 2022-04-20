using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// 해야할 일
    /// 이미 중상이면 중상체크 반복하지 않기(Clear!)
    /// 내상 만들기
    /// 중상 부위에 따른 이동 속도, 작업 속도 치료 되면 감소 스택 원래대로 되돌아오게 하기(보류)
    /// 상처감염 치료되면 이동 속도, 작업 속도 감소 스택 원래대로 되돌아오게 하기(보류)
    /// 상처감염 2일 지나면 진화하는 거 구현(대체)
    /// 정신병 구현(보류)
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

    [Header("플레이어 기본 정보")]
    [SerializeField]
    private GetPlayerScript playerStatusData;//ScriptableObject로 불러온 데이터

    private PlayerMove_GiJoo playerMove;
    private GameManager_Gijoo gameManager;

    public float playerHpNow;//플레이어의 현재 Hp
    public float playerMentalNow;//플레이어의 현재 정신력
    public float playerSpeedNow;//플레이어의 현재 이동 속도
    public float playerRunningSpeedNow;
    public float playerHandlingNow;//플레이어의 현재 작업 속도

    private float playerHpTimer;//플레이어가 얼마나 외상을 오래 입었는지 체크
    private int daySinceWoundInfection = 0;//상처 감염이 시작된 날짜
    private int playerBleedingCount;//플레이어의 과다출혈 스택
    private int illusionCount;//환각 스택
    private int hallucinationCount;//환청 스택
    private int woundInfectionCount;//상처 감염 스택
    private float playerInfectionPercent = 100f;//플레이어의 상처 감염 확률

    private bool isMinored = false;//경상인가? true 체크
    private bool isSerioused = false;//중상인가? true 체크
    private bool isWoundInfection = false;//상처감염이 시작되었는가? true 체크

    

    private void Awake()
    {
        playerHpNow = playerStatusData.PMHp;//플레이어의 현재 Hp를 Data 내에 저장된 최대 Hp로 초기화
        playerMentalNow = playerStatusData.PMMp;//플레이어의 현재 정신력을 Data 내에 저장된 최대 정신력으로 초기화
        playerSpeedNow = playerStatusData.PSpd;//플레이어의 현재 이동 속도를 Data 내에 저장된 이동속도로 초기화
        playerRunningSpeedNow = playerStatusData.PRSp;//플레이어의 현재 달리기 속도를 Data 내에 저장된 달리기속도로 초기화
        playerHandlingNow = playerStatusData.PMHS;//플레이어의 현재 작업 속도를 Data 내에 저장된 이동속도로 초기화
        playerMove = GetComponent<PlayerMove_GiJoo>();
        gameManager = FindObjectOfType<GameManager_Gijoo>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))//대미지 실험용 코드
        {
            Damaged(10);
            MentalDamaged(10);
        }
    }


    public void Damaged(float damage) //대미지를 입었을 때
    {
        playerHpNow -= damage; //Hp가 받은 대미지만큼 깎임
        if(Random.Range(0,100) >= 95)//맞을 때마다 5% 확률
        {
            Disease(); // 질병 실행
        }
        HpCheck(playerHpNow); //현재 Hp를 토대로 Hp를 체크함
    }

    public void Disease() // 질병
    {
        Debug.Log("넘모 아파잉");
       switch(Random.Range(0, 2))
        {
            case 0:
                Cold(); //감기
                break;
            case 1:
                Virus(); //바이러스
                break;
        }
    }

    public void Cold() // 감기
    {
        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //이동 속도가 5% 감소함
        playerRunningSpeedNow -= (playerRunningSpeedNow * 0.05f); //달리기 속도가 5% 감소함
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //작업 속도가 5% 감소함
    }

    public void Virus() //바이러스
    {

    }

    public void HpCheck(float _playerHpNow) //현재 HP 체크하는 코드
    {
        if (_playerHpNow <= playerStatusData.PMHp * 0.5) //플레이어의 현재 HP가 최대 HP의 절반 이하면 중상
        {
            if (!isSerioused)
            {
                isSerioused = true;
                WoundPhysicCheck();//중상인지 경상인지 체크
            }
        }
        else if (_playerHpNow <= playerStatusData.PMHp * 0.8) //플레이어의 현재 HP가 최대 HP의 80% 이하면 경상
        {
            if (!isMinored)
            {
                isMinored = true;
                WoundPhysicCheck();//중상인지 경상인지 체크
            }
        }
        else isMinored = isSerioused = false; //만약, Player의 현재 HP가 최대 HP의 80%보다 크면 중상도 경상도 아님
    }

    public void WoundPhysicCheck() //경상, 중상 정도 체크하는 코드
    {
        if (isSerioused) //중상이 True라면
        {
            Debug.Log("상처가 심각해... 우선 치료를 받아야겠어.");
            isMinored = false; //경상이 False가 됨
            StopCoroutine(MinorWoundPhysic());//경상일 때의 코루틴을 중단함
            SeriousWoundPhysicPos(); //중상 부위 체크
            StartCoroutine(SeriousWoundPhysic());//중상일 때의 코루틴을 실행함
        }
        else if (isMinored) //경상이 True고 중상이 False라면
        {
            Debug.Log("살짝 아픈 것 같은데, 뭐 상관 없겠지.");
            StopCoroutine(SeriousWoundPhysic());//중상일 때의 코루틴을 중단함
            StartCoroutine(MinorWoundPhysic());//경상을 때의 코루틴을 실행함
        }
        else //둘 다 False라면
        {
            StopAllCoroutines();//모든 코루틴을 중단함
            playerHpTimer = 0f;//체크 시간이 증가하는 도중에 끊겼을 수 있으므로 다시 0으로 초기화
            playerBleedingCount = 0;//과다출혈 스택이 0이 됨
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
        int where = Random.Range(0, 4); //확률, 각 각 동일하게 25%

        switch(where)
        {
            case (int)WherePos.arms:
                Debug.Log("윽, 팔이 좀 아픈 것 같은데..?");
                playerHandlingNow -= playerStatusData.PMHS * 0.1f;
                break; // 작업 속도 10% 줄어들게 하기

            case (int)WherePos.legs:
                Debug.Log("다리가 끊어질 것 같아..");
                playerMove.isCanRun = false;
                break; // 못 뛰게 하기

            case (int)WherePos.head:
                Debug.Log("머리가 깨질 듯이 아픈데..?");
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerMove.isCanRun = false;
                break; // 두 디버프 다 적용 됨

            case (int)WherePos.stomach:
                Debug.Log("윽, 배가....");
                playerHandlingNow -= playerStatusData.PMHS * 0.05f;
                playerMove.isCanRun = false;
                break; // 두 디버프 다 적용 됨

            default: break;
        }

    }

    public void PlayerWoundInfectionRandomCheck() //상처 감염 체크 코드
    {
        if (!isWoundInfection)
        {
            if (Random.Range(0, 100) >= playerInfectionPercent) //0부터 99의 수 중에 하나를 랜덤으로 고르고, 그 수가 확률보다 클 때 상처 감염이 발생함
            {
                woundInfectionCount = 1; //상처 감염 1단계 발생
                isWoundInfection = true;
            }
        }
    }

    public void WoundInfectionDayCount()
    {
        if(gameManager.dayCount - daySinceWoundInfection == 2 && daySinceWoundInfection != 0)// 상처 감염 발생 이후 2일이 지나면
        {
            woundInfectionCount += 1; //상처 감염 1단계 증가
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

    public void WoundInfection1Step() //상처감염 1단계, 게임 내 시간으로 2일이 지나면 2단계로 넘어가게 만들어야 함
    {
        daySinceWoundInfection = gameManager.dayCount;

        Debug.Log("상처가 점차 악화되고있습니다");

        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //이동 속도가 5% 감소함
        playerRunningSpeedNow -= (playerRunningSpeedNow * 0.05f); //달리기 속도가 5% 감소함
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //작업 속도가 5% 감소함
    }

    public void WoundInfection2Step() //상처감염 2단계, 게임 내 시간으로 2일이 지나면 괴사 단계로 넘어가게 만들어야 함
    {
        daySinceWoundInfection = gameManager.dayCount;

        Debug.Log("상처에 검은 빛이 돌기 시작했습니다.");

        playerSpeedNow -= (playerStatusData.PSpd * 0.05f); //이동 속도가 5% 추가로 감소함
        playerRunningSpeedNow -= (playerRunningSpeedNow * 0.05f); //달리기 속도가 5% 추가로 감소함
        playerHandlingNow -= (playerStatusData.PMHS * 0.05f); //작업 속도가 5% 추가로 감소함
    }

    public void WoundNecrosis() //상처 괴사
    {
        daySinceWoundInfection = 0;
        woundInfectionCount = 0;
        isWoundInfection = false;

        Debug.Log("상처가 썩어 문드러졌습니다.");

        playerStatusData.PSpd -= playerStatusData.PSpd * 0.1f; //Data 상의 이동속도가 영구적으로 10% 감소함
        playerStatusData.PRSp -= playerRunningSpeedNow * 0.1f; //Data 상의 달리기속도가 영구적으로 10% 감소함
        playerStatusData.PMHS -= playerStatusData.PMHS * 0.1f; //Data 상의 작업속도가 영구적으로 10% 감소함

    }

    public void PlayerDiedbyExcessiveBleedimg() //과다출혈로 죽음
    {
        if(playerBleedingCount >= 15) //매 1분마다 1씩 증가하는 과다 출혈 카운트가 15가 됐을 때, 즉 15분이 지났을 때 실행함
        {
            Debug.Log("당신은 과다출혈로 사망했습니다.");
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
            illusionCount = 3;
            hallucinationCount = 3;
            playerSpeedNow -= (playerStatusData.PSpd * 0.2f);
            playerRunningSpeedNow -= (playerStatusData.PRSp * 0.2f);
            playerHandlingNow -= (playerStatusData.PMHS * 0.2f);
            Debug.Log("정신 붕괴"); //정신 붕괴 상태이상이 일어남
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.25f) //만약 정신력이 25% 만큼 남았다면
        {
            if (Random.Range(0, 100) <= 74) //75% 확률로 정신병에 걸림
            {
                Whatpsychosis();
                Debug.Log("'정신력 위험 경고, 지금 당장 치료가 필요한 상태입니다'");
            }
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.5f) //만약 정신력이 50% 만큼 남았다면
        {
            if (Random.Range(0, 100) <= 49) //50% 확률로 정신병에 걸림
            {
                Whatpsychosis();
                Debug.Log("'정신력 위험도 : 2단계, 치료를 권고합니다'");
            }
        }
        else if (_playerMentalNow <= playerStatusData.PMMp * 0.75f) //만약 정신력이 75% 만큼 남았다면
        {
            if (Random.Range(0, 100) <= 24) //25% 확률로 정신병에 걸림
            {
                Whatpsychosis();
                Debug.Log("'정신력 위험도 : 1단계'");
            }
        }
        else //정신력이 75%보다 크다면
        {
            Debug.Log("정상 상태"); //정상 상태임
        }
    }

    public void Whatpsychosis() //어떤 정신병을 얻었는가
    {
        int whatPsychosis = Random.Range(0, 2);
        switch(whatPsychosis)
        {
            case (int)WhatPsychosis.illusion: //환각 정신병이면
                if (illusionCount < 3) //환각 최대 중첩 3단계 제한
                {
                    Debug.Log("환각이 발생했습니다.");
                    illusionCount += 1; //환각 단계가 1 오른다
                    IfIllusion(); //환각 효과 코드 실행
                }
                break;
            case (int)WhatPsychosis.hallucination: //환청 정신병이면
                if (hallucinationCount < 3) //환청 최대 중첩 3단계 제한
                {
                    Debug.Log("환청이 발생했습니다.");
                    hallucinationCount += 1; //환청 단계가 1 오른다
                    IfHallucination(); //환청 효과 코드 실행
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 이 밑으로 일단 보류
    /// </summary>
    public void IfIllusion()//여기서 환각 다룸
    {
        switch(illusionCount)//환각 카운트가 n일 때 n중첩 효과가 나옴
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

    public void IfHallucination()//여기서 환청 다룸
    {
        switch (hallucinationCount)//환청 카운트가 n일 때 n중첩 효과가 나옴
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
