using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        // 오른쪽으로 계속 이동하기
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Enemy.enemyDeath++;
        }

        else if (collider.gameObject.tag == "BossMonster")
        {
            BossMonster bm = GameObject.Find("BossMonster").GetComponent<BossMonster>();
            bm.BossOnDamage(attackPower);
        }

        // 다른 물체와 부딪혔다면 Bullet은 사라진다.
        Destroy(this.gameObject);
    }
}
