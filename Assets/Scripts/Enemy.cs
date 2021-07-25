using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속력 변수
    public float speed = 5;

    // Enemy 죽은 수
    public static int enemyDeath = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // *왼쪽으로 계속 이동하기
        // 1. 왼쪽으로 방향을 만들고
        Vector3 dir = Vector3.left;
        // 2. 이동한다.
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // *다른 오브젝트와 부딪혔을 때
        // 1. 만약 닿은 물체가 바닥이라면 무시하고
        if (collision.gameObject.tag == "Floor")
            return;
        // 다른 물체라면 Enemy가 죽는다.
        Destroy(gameObject);
        // 2. 만약 플레이어와 부딪하면, 
        // 플레이어는 체력이 1만큼 깎이고, 적 데스 수가 하나 올라간다.
        if (collision.gameObject.tag == "Player")
        {
            PlayerMove.hp = PlayerMove.hp - 1;
            enemyDeath++;
        }
    }
}
