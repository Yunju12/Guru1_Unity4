using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBallManager : MonoBehaviour
{
    // SmallBall ���� ����
    public GameObject smallBallFactory;

    // �����ð� ����
    public float createTime = 3;

    // ����ð� ����
    float currentTime;

    // �������� ������Ʈ ����
    BossMonster bm;

    void Start()
    {
        // �������� ������Ʈ ��������
        bm = GetComponent<BossMonster>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // * ������ ���� �ð��� �ѹ��� �� �����ϱ�
        // 1. ����ð��� ���.
        currentTime += Time.deltaTime;

        // 2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            // 3. ���� �����忡�� �����Ѵ�.
            GameObject ball = Instantiate(smallBallFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            ball.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
        }
    }
}
