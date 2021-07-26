using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    // 보스몬스터가 위치할 자리
    Vector3 bossPosition = new Vector3(5.85f, -1.48f, 0);

    // HP 바 변수
    public GameObject HPBar;

    // 파이어볼 매니져 변수1
    public GameObject FireBallManager1;

    // 파이어볼 매니져 변수2
    public GameObject FireBallManager2;

    // 파이어볼 매니져 변수3
    public GameObject FireBallManager3;

    // 파이어볼 매니져 변수4
    public GameObject FireBallManager4;

    // 체력 변수
    public int bossHp;

    // 최대 체력 변수
    public int maxHp = 30;

    // 슬라이더 바
    public Slider hpSlider;

    // 애니메이션 변수
    Animator ani;

    void Start()
    {
        // 체력 변수 초기화
        bossHp = maxHp;

        // 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // 만약 enemyDeath가 5 이상이 된다면, 
        if (Enemy.enemyDeath >= 5)
        {
            // 보스 몬스터를 활성화 시킨 뒤,
            gameObject.SetActive(true);

            // 보스몬스터와 보스몬스터의 HP 바가 등장한다.
            StartCoroutine("BossAppear");

            // 슬라이더의 value를 체력 비율로 적용한다.
            hpSlider.value = (float)bossHp / (float)maxHp;
        }

        if (bossHp <= 0)
        {
            ani.SetTrigger("");
        }
    }

    // 보스몬스터와 보스몬스터의 HP 바가 등장하는 코루틴 함수
    IEnumerator BossAppear()
    {
        // 3초 대기 후
        yield return new WaitForSeconds(3f);

        // 보스몬스터의 HP 바를 활성화한다.
        HPBar.SetActive(true);

        // 다시 2초 대기 후
        yield return new WaitForSeconds(2f);

        // 보스 몬스터가 날라와서 정해진 자리에 멈춰 선다.
        transform.position = Vector3.Slerp(transform.position, bossPosition, 0.008f);

        // 파이어볼 매니져를 활성화한다.
        FireBallManager1.SetActive(true);
        FireBallManager2.SetActive(true);
        FireBallManager3.SetActive(true);
        FireBallManager4.SetActive(true);
    }

    // 보스몬스터 피격 함수
    public void BossOnDamage(int value)
    {
        bossHp -= value;

        if (bossHp < 0)
        {
            bossHp = 0;
        }
    }
}
