using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 아이템 공장 변수
    public GameObject itemFactory;

    // 생성시간 변수
    public float createTime = 3;

    // 경과시간 변수
    float currentTime;

    // 최소 시간 변수
    public float minTime = 5;

    // 최대 시간 변수
    public float maxTime = 10;

    public GameObject i1;

    public GameObject i2;

    public GameObject i3;

    GameObject item;

    void Start()
    {


        // 생성시간을 최소 시간과 최대 시간 사이에서 랜덤으로 정한다.
        //createTime = Random.Range(minTime, maxTime);
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
            //int randomValue = Random.Range(0, 3);

            //if (randomValue == 0)
            //{
                //item = i1;
            //}
            //else if (randomValue == 1)
            //{
                //item = i2;
            //}
            //else
            //{
                //item = i3;
            //}

            // 3. 적을 적공장에서 생성한다.
            item = Instantiate(itemFactory);

            // 4. 생성된 적을 배치한다.
            item.transform.position = transform.position;

            // 5. 경과시간을 초기화하고 다시 랜덤으로 정한다.
            currentTime = 0;
            //createTime = Random.Range(minTime, maxTime);
        }
    }
}
