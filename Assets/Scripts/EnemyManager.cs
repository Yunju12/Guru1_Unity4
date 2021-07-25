using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 적공장 변수
    public GameObject enemyFactory;

    // 생성시간 변수
    public float createTime = 2;

    // 경과시간 변수
    float currentTime;

    // 최소 시간 변수
    public float minTime = 1;

    // 최대 시간 변수
    public float maxTime = 5;

    void Start()
    {
        // 생성시간을 최소 시간과 최대 시간 사이에서 랜덤으로 정한다.
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // *일정한 생성시간에 한번씩 적을 생성한다.
        // 1. 경과시간을 잰다.
        currentTime += Time.deltaTime;

        // 2. 만약 경과시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            // 3. 적을 적공장에서 생성한다.
            GameObject enemy = Instantiate(enemyFactory);

            // 4. 생성된 적을 배치한다.
            enemy.transform.position = transform.position;

            // 5. 경과시간을 초기화하고 다시 랜덤으로 정한다.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }

        if (Enemy.enemyDeath >= 5)
        {
            gameObject.SetActive(false);
        }
    }
}
