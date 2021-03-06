using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    // 속도 변수
    public float speed = 10;

    // 방향 변수
    Vector3 dir;

    // 공격력 변수
    public int attackPower = 2;

    void Start()
    {

        // * 생성될 때 30% 확률로 플레이어 방향, 나머지 확률로 왼쪽 방향으로 가게 하기
        // 1. 랜덤값을 0 에서 9 중 하나로 설정한다.
        int randomValue = Random.Range(0, 10);

        // 2. 만약 랜덤값이 0이면
        if (randomValue == 0)
        {
            // 플레이어의 위치를 찾고,
            GameObject target = GameObject.Find("Player");

            // 플레이어의 방향 쪽으로 날아간다.
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        // 3. 그렇지 않으면 왼쪽으로 날아간다.
        else
        {
            dir = Vector3.left;
        }
            
    }

    void Update()
    {
        // 게임 상태가 Run 이 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // 정해진 방향으로 계속 이동한다.
        transform.position += dir * speed * Time.deltaTime;
    }

    // * 공격
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
