using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    // �������Ͱ� ��ġ�� �ڸ�
    Vector3 bossPosition = new Vector3(7.6f, -1.4f, 0);

    // �̵� �ڸ�
    Vector3 movePosition = new Vector3(7.6f, 3.8f, 0);

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

    // ���̾ �Ŵ��� ����4
    public GameObject s1;

    // ���̾ �Ŵ��� ����4
    public GameObject s2;

    // ü�� ����
    public int bossHp;

    // �ִ� ü�� ����
    public int maxHp = 30;

    // �����̴� ��
    public Slider hpSlider;

    // ���� ��ġ ����
    //Vector3 position;

    // ���Ʒ��� �̵������� y�� �ִ밪 ����
    public float upMax = 1.0f;

    // ���Ʒ��� �̵������� y�� �ִ밪 ����
    public float downMax = 1.0f;

    // �̵� �ӵ� ����
    public float speed = 3.0f;

    // �ִϸ��̼� ����
    Animator ani;

    GameObject player;

    void Start()
    {
        // ü�� ���� �ʱ�ȭ
        bossHp = maxHp;

        // �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // �����̴��� value�� ü�� ������ �����Ѵ�.
        hpSlider.value = (float)bossHp / (float)maxHp;

        // ���� enemyDeath�� 5 �̻��� �ȴٸ�, 
        if (Enemy.enemyDeath >= Enemy.maxEnemyDeath)
        {
            // ���� ���͸� Ȱ��ȭ ��Ų ��,
            gameObject.SetActive(true);

            // �������Ϳ� ���������� HP �ٰ� �����Ѵ�.
            StartCoroutine("BossAppear");
        }

        if (bossHp <= 0)
        {
            ani.SetTrigger("ToDie");
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
        s1.SetActive(true);
        s2.SetActive(true);
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
