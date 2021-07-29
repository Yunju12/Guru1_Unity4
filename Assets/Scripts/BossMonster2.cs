using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster2 : MonoBehaviour
{
    // ���� ü�� ����
    public static int bossHp;

    // �ִ� ü�� ����
    public int maxHp = 30;

    // �����̴� ��
    public Slider hpSlider;

    // HP �� ����
    public GameObject HPBar;

    // �������Ͱ� ��ġ�� �ڸ�
    Vector3 bossPosition = new Vector3(7.6f, -1.4f, 0);

    // �� �Ŵ��� ����1
    public GameObject BallManager1;

    // �� �Ŵ��� ����2
    public GameObject BallManager2;

    // �� �Ŵ��� ����3
    public GameObject BallManager3;

    // �� �Ŵ��� ����4
    public GameObject BallManager4;

    // (SmallThunder ����) ���� �� �Ŵ��� ����1
    public GameObject SmallBallManager1;

    // (SmallThunder ����) ���� �� �Ŵ��� ����2
    public GameObject SmallBallManager2;

    // �ִϸ����� ������Ʈ ����
    Animator ani;

    void Start()
    {
        // ���� ü�� ������ �ʱ�ȭ�Ѵ�.
        bossHp = maxHp;

        // �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // �����̴��� ���� ���� ü���� ������ �����Ѵ�.
        hpSlider.value = (float)bossHp / (float)maxHp;

        // ���� �� óġ ���� �� �ִ� óġ �� �̻��� �Ǹ�,
        if (Enemy.enemyDeath >= Enemy.maxEnemyDeath)
        {
            // ���� ���� �ڷ�ƾ �Լ��� �����Ѵ�.
            StartCoroutine("BossAppear");
        }

        // ���� ���������� ü���� 0 ���ϰ� �Ǹ�,
        if (bossHp <= 0)
        {
            // ���ϸ������� �Ķ���� ToDie �� �����Ѵ�.
            ani.SetTrigger("ToDie");
        }
    }

    // * �������Ϳ� ���������� HP �ٰ� �����ϴ� �ڷ�ƾ �Լ�
    IEnumerator BossAppear()
    {
        // 1. 3�� ��� ��
        yield return new WaitForSeconds(3f);

        // 2. ���������� HP �ٸ� Ȱ��ȭ�Ѵ�.
        HPBar.SetActive(true);

        // 3. �ٽ� 2�� ��� ��
        yield return new WaitForSeconds(2f);

        // 4. ���� ���Ͱ� ����ͼ� ������ �ڸ��� ���� ����.
        transform.position = Vector3.Slerp(transform.position, bossPosition, 0.008f);

        // 5. �� �Ŵ����� Ȱ��ȭ�Ѵ�.
        BallManager1.SetActive(true);
        BallManager2.SetActive(true);
        BallManager3.SetActive(true);
        BallManager4.SetActive(true);
        SmallBallManager1.SetActive(true);
        SmallBallManager2.SetActive(true);
    }

    // * �������� �ǰ� �Լ�
    public void BossOnDamage(int value)
    {
        // ����� ���ݷ¸�ŭ ���� ü���� �پ���.
        bossHp -= value;

        // ���� ���� ü���� 0 �̸��̶��,
        if (bossHp < 0)
        {
            // ���� ü�� ���� 0 ���� �Ѵ�.
            bossHp = 0;
        }
    }
}