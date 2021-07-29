using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // �����ð� ����
    public float createTime = 2;

    // �ּ� �ð� ����
    public float minTime = 1;

    // �ִ� �ð� ����
    public float maxTime = 5;

    // ��� �ð� ����
    float currentTime;

    // �� ���� ����
    public GameObject enemyFactory;

    void Start()
    {
        // ���� �ð��� �ּ� �ð��� �ִ� �ð� ���̿��� �������� ���Ѵ�.
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // ���� ���°� Run ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // *������ ���� �ð��� �ѹ��� �� �����ϱ�.
        // 1. ��� �ð��� ���.
        currentTime += Time.deltaTime;

        // 2. ���� ��� �ð��� ���� �ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            // 3. ���� �� ���忡�� �����Ѵ�.
            GameObject enemy = Instantiate(enemyFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            enemy.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }

        // �� óġ ���� �� �ִ� óġ ���� ���ų� �׺��� Ŭ ��
        if (Enemy.enemyDeath >= Enemy.maxEnemyDeath)
        {
            // ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
            gameObject.SetActive(false);
        }
    }
}
