using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 생성시간 변수
    public float createTime = 2;

    // 최소 시간 변수
    public float minTime = 1;

    // 최대 시간 변수
    public float maxTime = 5;

    // 경과 시간 변수
    float currentTime;

    // 적 공장 변수
    public GameObject enemyFactory;

    void Start()
    {
        // 생성 시간을 최소 시간과 최대 시간 사이에서 랜덤으로 정한다.
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // 게임 상태가 Run 상태가 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // *일정한 생성 시간에 한번씩 적 생성하기.
        // 1. 경과 시간을 잰다.
        currentTime += Time.deltaTime;

        // 2. 만약 경과 시간이 생성 시간을 초과하면
        if (currentTime > createTime)
        {
            // 3. 적을 적 공장에서 생성한다.
            GameObject enemy = Instantiate(enemyFactory);

            // 4. 생성된 적을 배치한다.
            enemy.transform.position = transform.position;

            // 5. 경과시간을 초기화하고 다시 랜덤으로 정한다.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }

        // 적 처치 수가 적 최대 처치 수와 같거나 그보다 클 때
        if (Enemy.enemyDeath >= Enemy.maxEnemyDeath)
        {
            // 오브젝트를 비활성화한다.
            gameObject.SetActive(false);
        }
    }
}
