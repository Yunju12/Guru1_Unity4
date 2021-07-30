using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 속도 변수
    public float speed = 10;

    // 공격력
    public int attackPower = 2;

    // 보스 몬스터 컴포넌트 변수
    BossMonster bm;

    public GameManager gm;

    void Start()
    {
        bm = GetComponent<BossMonster>();
    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState == GameManager.GameState.GameOver)
        {
            this.gameObject.SetActive(false);
        }

        // 오른쪽으로 계속 이동하기
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 다른 물체와 부딪혔다면 Bullet은 사라진다.
        Destroy(this.gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy.enemyDeath++;
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().totalPoint += 50;
        }

        else if (collision.gameObject.tag == "BossMonster")
        {
            
            collision.gameObject.GetComponent<BossMonster>().BossOnDamage(attackPower);
        }
    }
}
