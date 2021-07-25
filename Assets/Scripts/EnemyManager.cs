using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ������ ����
    public GameObject enemyFactory;

    // �����ð� ����
    public float createTime = 2;

    // ����ð� ����
    float currentTime;

    // �ּ� �ð� ����
    public float minTime = 1;

    // �ִ� �ð� ����
    public float maxTime = 5;

    void Start()
    {
        // �����ð��� �ּ� �ð��� �ִ� �ð� ���̿��� �������� ���Ѵ�.
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // *������ �����ð��� �ѹ��� ���� �����Ѵ�.
        // 1. ����ð��� ���.
        currentTime += Time.deltaTime;

        // 2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            // 3. ���� �����忡�� �����Ѵ�.
            GameObject enemy = Instantiate(enemyFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            enemy.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }

        if (Enemy.enemyDeath >= 5)
        {
            gameObject.SetActive(false);
        }
    }
}
