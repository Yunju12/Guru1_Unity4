using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // 파이어볼(or 워터볼) 공장 변수
    public GameObject ballFactory;

    // 생성시간 변수
    public float createTime = 2;

    // 경과시간 변수
    float currentTime;

    // 최소 시간 변수
    public float minTime = 0.5f;

    // 최대 시간 변수
    public float maxTime = 1;

    // 보스몬스터 컴포넌트 변수
    BossMonster bm;

    void Start()
    {
        // 생성시간을 최소 시간과 최대 시간 사이에서 랜덤으로 정한다.
        createTime = Random.Range(minTime, maxTime);

        // 보스몬스터 텀포넌트 가져오기
        bm = GetComponent<BossMonster>();
    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // * 일정한 생성 시간에 한번씩 적 생성하기
        // 1. 경과시간을 잰다.
        currentTime += Time.deltaTime;

        // 2. 만약 경과시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            // 3. 적을 적공장에서 생성한다.
            GameObject fireball = Instantiate(ballFactory);

            // 4. 생성된 적을 배치한다.
            fireball.transform.position = transform.position;

            // 5. 경과시간을 초기화하고 다시 랜덤으로 정한다.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }

        //if (bm.bossHp <= 0)
        //{
           //gameObject.SetActive(false);
        //}
    }
}
