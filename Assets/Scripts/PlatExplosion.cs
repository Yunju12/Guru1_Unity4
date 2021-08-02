using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatExplosion : MonoBehaviour
{
    // 현재 시간
    float currentTime = 0;

    // 게임 매니저 변수
    public PlatGameManager PlatGameManager;

    void Start()
    {

    }

    void Update()
    { 
        // 생성된 때로부터 1초가 경과되면 사라진다.
        if (currentTime >= 0.8f)
        {
            Destroy(gameObject);
        }
        currentTime += Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 만약 Enemy 와 부딪히면, 포인트 100점을 얻고 부딪힌 Enemy 는 죽는다.
        if (collision.CompareTag("Enemy"))
        {
            //PlatGameManager.stagePoint += 100;
            collision.GetComponent<PlatEnemyMove>().OnDamaged();
        }
    }
}
