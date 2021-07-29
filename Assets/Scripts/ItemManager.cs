using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // ������ ���� ����
    public GameObject itemFactory;

    // �����ð� ����
    public float createTime = 3;

    // ����ð� ����
    float currentTime;

    // �ּ� �ð� ����
    public float minTime = 5;

    // �ִ� �ð� ����
    public float maxTime = 10;

    void Start()
    {
        
            
        // �����ð��� �ּ� �ð��� �ִ� �ð� ���̿��� �������� ���Ѵ�.
        //createTime = Random.Range(minTime, maxTime);
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
            GameObject item = Instantiate(itemFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            item.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
