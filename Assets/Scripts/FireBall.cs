using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // 속도 변수
    public float speed = 10;

    // 공격력
    public int attackPower = 2;

    void Start()
    {

    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // * 왼쪽으로 계속 이동하기
        // 1. 왼쪽으로 방향을 만들고
        Vector3 dir = Vector3.left;
        // 2. 이동한다.
        transform.position += dir * speed * Time.deltaTime;
    }

    // 만약 플레이어와 부딪히면
    // 플레이어의 체력이 2만큼 깎인다.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 다른 물체와 부딪혔다면 FireBall은 사라진다.
        Destroy(this.gameObject);

        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();
            pm.OnDamage(attackPower);
        }
    }
}
