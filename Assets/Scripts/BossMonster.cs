using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    // 보스몬스터가 위치할 자리
    Vector3 bossPosition = new Vector3(5.63f, -0.57f, -1);

    void Start()
    {
        // 비활성화 상태로 시작한다.
        //gameObject.SetActive(false);
    }

    void Update()
    {
        // 만약 enemyDeath가 5 이상이 된다면, 
        if (Enemy.enemyDeath >= 5)
        {
            // 오브젝트가 활성화되고,
            //gameObject.SetActive(true);

            // 게임 화면 안으로 포물선 모양으로 날아와서, 
            // 정해진 위치로 오면 멈춰 선다.
            transform.position = Vector3.Slerp(transform.position, bossPosition, 0.03f);

        }

        // 게임 화면으로 걸어들어와서
        // 플레이어의 공격을 받으면 체력이 줄어든다. 
    }
}
