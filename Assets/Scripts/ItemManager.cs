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

    public GameObject i1;

    public GameObject i2;

    public GameObject i3;

    GameObject item;

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
            int randomValue = Random.Range(0, 3);

            if (randomValue == 0)
            {
                item = i1;
            }
            else if (randomValue == 1)
            {
                item = i2;
            }
            else
            {
                item = i3;
            }

            // 3. ���� �����忡�� �����Ѵ�.
            item = Instantiate(itemFactory);

            // 4. ������ ���� ��ġ�Ѵ�.
            item.transform.position = transform.position;

            // 5. ����ð��� �ʱ�ȭ�ϰ� �ٽ� �������� ���Ѵ�.
            currentTime = 0;
            //createTime = Random.Range(minTime, maxTime);
        }
    }
}
