using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    // ���̾ ���� ����
    public GameObject fireBallFactory;

    // �����ð� ����
    public float createTime = 2;

    // ����ð� ����
    float currentTime;

    // �ּ� �ð� ����
    public float minTime = 0.5f;

    // �ִ� �ð� ����
    public float maxTime = 1;

    // ���� ���� ������Ʈ ����
    BossMonster bm;

    void Start()
    {
        // �����ð��� �ּ� �ð��� �ִ� �ð� ���̿��� �������� ���Ѵ�.
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ�
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // *������ �����ð��� �ѹ��� �� �����ϱ�.
        // 1. ����ð��� ���.
        currentTime += Time.deltaTime;

        // 2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            // 3. ���� �����忡�� �����Ѵ�.
            GameObject fireball = Instantiate(fireBallFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            fireball.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }

        BossMonster bm = GameObject.Find("BossMonster").GetComponent<BossMonster>();

        if (bm.bossHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
