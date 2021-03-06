using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : MonoBehaviour
{
    // 속력 변수
    public float speed = 5;

    // 적 처치 수
    public static int enemyDeath = 0;

    // 적 최대 처치 수
    public static int maxEnemyDeath = 10;

    // 공격력 변수
    public int attackPower = 2;

    // PlayerMove 컴포넌트 변수
    PlayerMove_Slime pms;

    void Start()
    {
        pms = GetComponent<PlayerMove_Slime>();
    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단
        if (GameManager_Slime.gm.gState != GameManager_Slime.GameState.Run)
        {
            return;
        }

        // *왼쪽으로 계속 이동하기
        // 1. 왼쪽으로 방향을 만들고
        Vector3 dir = Vector3.left;
        // 2. 이동한다.
        transform.position += dir * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 1) * 135 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 다른 물체와 부딪혔다면 Enemy는 죽는다.
        Destroy(this.gameObject);

        // 만약 플레이어와 부딪히면, 
        // 플레이어의 체력이 2만큼 깎인다.
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove_Slime>().OnDamage(attackPower);
        }
    }
}
