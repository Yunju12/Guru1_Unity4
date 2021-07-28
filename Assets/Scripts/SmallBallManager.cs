using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBallManager : MonoBehaviour
{
    // SmallBall 공장 변수
    public GameObject smallBallFactory;

    // 생성시간 변수
    public float createTime = 3;

    // 경과시간 변수
    float currentTime;

    // 보스몬스터 컴포넌트 변수
    BossMonster bm;

    void Start()
    {
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
            GameObject ball = Instantiate(smallBallFactory);

            // 4. 생성된 적을 배치한다.
            ball.transform.position = transform.position;

            // 5. 경과시간을 초기화하고 다시 랜덤으로 정한다.
            currentTime = 0;
        }
    }
}
