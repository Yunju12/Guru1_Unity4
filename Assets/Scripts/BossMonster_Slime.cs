using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster_Slime : MonoBehaviour
{
    // 보스 체력 변수
    public static int bossHp;

    // 최대 체력 변수
    public int maxHp = 30;

    // 슬라이더 바
    public Slider hpSlider;

    // HP 바 변수
    public GameObject HPBar;

    // 보스몬스터가 위치할 자리
    Vector3 bossPosition = new Vector3(7.6f, -1.4f, 0);

    // 볼 매니져 변수1
    public GameObject ThunderManager1;

    // 볼 매니져 변수2
    public GameObject ThunderManager2;

    // 볼 매니져 변수3
    public GameObject ThunderManager3;

    // 볼 매니져 변수4
    public GameObject ThunderManager4;

    // (SmallThunder 전용) 작은 볼 매니져 변수1
    public GameObject SmallBallManager1;

    // (SmallThunder 전용) 작은 볼 매니져 변수2
    public GameObject SmallBallManager2;

    // 애니메이터 컴포넌트 변수
    Animator anim;

    void Start()
    {
        // 보스 체력 변수를 초기화한다.
        bossHp = maxHp;

        // 애니메이션 컴포넌트를 받아온다.
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 슬라이더의 값에 보스 체력의 비율을 적용한다.
        hpSlider.value = (float)bossHp / (float)maxHp;

        // 만약 적 처치 수가 적 최대 처치 수 이상이 되면,
        if (Enemy_Slime.enemyDeath >= Enemy_Slime.maxEnemyDeath)
        {
            // 보스 등장 코루틴 함수를 실행한다.
            StartCoroutine("BossAppear");
        }

        // 만약 보스몬스터의 체력이 0 이하가 되면,
        if (bossHp <= 0)
        {
            // 에니메이터의 파라미터 ToDie 를 실행한다.
            anim.SetTrigger("ToDie");
        }
    }

    // * 보스몬스터와 보스몬스터의 HP 바가 등장하는 코루틴 함수
    IEnumerator BossAppear()
    {
        // 1. 3초 대기 후
        yield return new WaitForSeconds(3f);

        // 2. 보스몬스터의 HP 바를 활성화한다.
        HPBar.SetActive(true);

        // 3. 다시 2초 대기 후
        yield return new WaitForSeconds(2f);

        // 4. 보스 몬스터가 날라와서 정해진 자리에 멈춰 선다.
        transform.position = Vector3.Slerp(transform.position, bossPosition, 0.008f);

        // 5. 볼 매니저를 활성화한다.
        ThunderManager1.SetActive(true);
        ThunderManager2.SetActive(true);
        ThunderManager3.SetActive(true);
        ThunderManager4.SetActive(true);
        SmallBallManager1.SetActive(true);
        SmallBallManager2.SetActive(true);
   
    }

    // * 보스몬스터 피격 함수
    public void BossOnDamage(int value)
    {
        // 상대의 공격력만큼 보스 체력이 줄어든다.
        bossHp -= value;

        // 만약 보스 체력이 0 미만이라면,
        if (bossHp < 0)
        {
            // 보스 체력 값을 0 으로 한다.
            bossHp = 0;
        }
    }
}
