using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderManager : MonoBehaviour
{
    // ���̾(or ���ͺ�) ���� ����
    public GameObject ballFactory;

    // �����ð� ����
    public float createTime = 2;

    // ����ð� ����
    float currentTime;

    // �ּ� �ð� ����
    public float minTime = 0.5f;

    // �ִ� �ð� ����
    public float maxTime = 1;

    void Start()
    {
        // �����ð��� �ּ� �ð��� �ִ� �ð� ���̿��� �������� ���Ѵ�.
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager_Slime.gm.gState != GameManager_Slime.GameState.Run)
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
            GameObject ball = Instantiate(ballFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            ball.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
