using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("�÷��̾� �⺻ ����")]
    [SerializeField]
    private float playerHpMax;
    [SerializeField]
    private float playerMentalMax;

    public float playerHpNow;
    public float playerMentalNow;

    private float playerHpTimer;
    private float playerInfectionPercent = 100f;

    private bool isMinored = false;
    private bool isSerioused = false;

    private PlayerMove playerMove;

    private void Awake()
    {
        playerHpNow = playerHpMax;
        playerMentalNow = playerMentalMax;
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        HpCheck(playerHpNow);
        MentalCheck(playerMentalNow);
    }

    public void HpCheck(float playerHpNow) //���� HP üũ�ϴ� �ڵ�
    {
        if (playerHpNow <= playerHpMax / 2)
        {
            isSerioused = true;
        }
        else if (playerHpNow <= (playerHpMax * 4) / 5)
        {
            isMinored = true;
        }
        else isMinored = isSerioused = false;
    }

    public void WoundPhysicCheck() //���, �߻� ���� üũ�ϴ� �ڵ�
    {
        if (isMinored)
        {
            StartCoroutine(MinorWoundPhysic());
        }
        if (isSerioused)
        {
            isMinored = false;
            StopCoroutine(MinorWoundPhysic());
            StartCoroutine(SeriousWoundPhysic());
        }
        else if(!isMinored && !isSerioused)
        {
            StopAllCoroutines();
            playerInfectionPercent = 100f;
        }

    }

    public IEnumerator MinorWoundPhysic() //���(�ܻ�)
    {
        while (true)
        {
            playerHpTimer = 0f;
            while (playerHpTimer <= 120f)
            {
                playerHpTimer += Time.unscaledTime;
                yield return null;
            }
            playerInfectionPercent -= 5f;
            PlayerWoundInfectionRandomCheck();
        }
    }
    public IEnumerator SeriousWoundPhysic() //�߻�(�ܻ�)
    {
        while (true)
        {
            playerHpTimer = 0f;
            while (playerHpTimer <= 60f)
            {
                playerHpTimer += Time.unscaledTime;
                yield return null;
            }
            playerInfectionPercent -= 10f;
            PlayerWoundInfectionRandomCheck();
        }
    }

    public void WoundInfection() //��ó����
    {

    }

    public void PlayerWoundInfectionRandomCheck() //��ó���� üũ �ڵ�
    {
        float randomPercent = Random.Range(0, 99);
        if(randomPercent >= playerInfectionPercent)
        {
            WoundInfection();
        }
    }

    public void MentalCheck(float playerMentalNow)//���� ���ŷ� üũ�ϴ� �ڵ�
    {
        if(playerMentalNow <= playerMentalMax / 20)
        {

        }
        else if(playerMentalNow <= playerMentalMax / 4)
        {

        }
        else if (playerMentalNow <= playerMentalMax / 2)
        {

        }
        else if (playerMentalNow <= (playerMentalMax * 3) / 4)
        {

        }
        else
        {

        }
    }

}
