using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    // 속도 변수
    public float speed = 10;

    // 방향 변수
    Vector3 dir;

    // 공격력 변수
    public int attackPower = 2;

    // 
    PlayerMove_Slime pms;

    void Start()
    {
        pms = GetComponent<PlayerMove_Slime>();
    }

    void Update()
    {
        // 게임 상태가 Run 이 아니면 업데이트 함수를 중단한다.
        if (GameManager_Slime.gm.gState != GameManager_Slime.GameState.Run)
        {
            return;
        }

        // 정해진 방향으로 계속 이동한다.
        dir = Vector3.left;
        transform.position += dir * speed * Time.deltaTime;
    }

    // * 공격
    // 만약 플레이어와 부딪히면
    // 플레이어의 체력이 2만큼 깎인다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 다른 물체와 부딪혔다면 FireBall은 사라진다.
        Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerMove_Slime>().OnDamage(attackPower);
        }
    }
}
