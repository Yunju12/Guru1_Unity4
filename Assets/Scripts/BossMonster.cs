using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    // �������Ͱ� ��ġ�� �ڸ�
    Vector3 bossPosition = new Vector3(5.85f, -1.48f, 0);

    // HP �� ����
    public GameObject HPBar;

    // ���̾ �Ŵ��� ����1
    public GameObject FireBallManager1;

    // ���̾ �Ŵ��� ����2
    public GameObject FireBallManager2;

    // ���̾ �Ŵ��� ����3
    public GameObject FireBallManager3;

    // ���̾ �Ŵ��� ����4
    public GameObject FireBallManager4;

    // ü�� ����
    public int bossHp;

    // �ִ� ü�� ����
    public int maxHp = 30;

    // �����̴� ��
    public Slider hpSlider;

    // �ִϸ��̼� ����
    Animator ani;

    void Start()
    {
        // ü�� ���� �ʱ�ȭ
        bossHp = maxHp;

        // �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // ���� enemyDeath�� 5 �̻��� �ȴٸ�, 
        if (Enemy.enemyDeath >= 5)
        {
            // ���� ���͸� Ȱ��ȭ ��Ų ��,
            gameObject.SetActive(true);

            // �������Ϳ� ���������� HP �ٰ� �����Ѵ�.
            StartCoroutine("BossAppear");

            // �����̴��� value�� ü�� ������ �����Ѵ�.
            hpSlider.value = (float)bossHp / (float)maxHp;
        }

        if (bossHp <= 0)
        {
            ani.SetTrigger("");
        }
    }

    // �������Ϳ� ���������� HP �ٰ� �����ϴ� �ڷ�ƾ �Լ�
    IEnumerator BossAppear()
    {
        // 3�� ��� ��
        yield return new WaitForSeconds(3f);

        // ���������� HP �ٸ� Ȱ��ȭ�Ѵ�.
        HPBar.SetActive(true);

        // �ٽ� 2�� ��� ��
        yield return new WaitForSeconds(2f);

        // ���� ���Ͱ� ����ͼ� ������ �ڸ��� ���� ����.
        transform.position = Vector3.Slerp(transform.position, bossPosition, 0.008f);

        // ���̾ �Ŵ����� Ȱ��ȭ�Ѵ�.
        FireBallManager1.SetActive(true);
        FireBallManager2.SetActive(true);
        FireBallManager3.SetActive(true);
        FireBallManager4.SetActive(true);
    }

    // �������� �ǰ� �Լ�
    public void BossOnDamage(int value)
    {
        bossHp -= value;

        if (bossHp < 0)
        {
            bossHp = 0;
        }
    }
}
