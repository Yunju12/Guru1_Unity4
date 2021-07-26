using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속력 변수
    public float speed = 5;

    // Enemy 죽은 수
    public static int enemyDeath = 0;

    // 플레이어 변수
    GameObject player;

    // 공격력 변수
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

        // *왼쪽으로 계속 이동하기
        // 1. 왼쪽으로 방향을 만들고
        Vector3 dir = Vector3.left;
        // 2. 이동한다.
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // *다른 오브젝트와 부딪혔을 때
        

        // 2. 만약 플레이어와 부딪하면, 
        // 플레이어는 체력이 2만큼 깎이고, 적 데스 수가 하나 올라간다.
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();
            pm.OnDamage(attackPower);
        }

        // 다른 물체와 부딪혔다면 Enemy는 죽는다.
        Destroy(gameObject);
    }
}
