using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    // �������Ͱ� ��ġ�� �ڸ�
    Vector3 bossPosition = new Vector3(5.63f, -0.57f, -1);

    void Start()
    {
        // ��Ȱ��ȭ ���·� �����Ѵ�.
        //gameObject.SetActive(false);
    }

    void Update()
    {
        // ���� enemyDeath�� 5 �̻��� �ȴٸ�, 
        if (Enemy.enemyDeath >= 5)
        {
            // ������Ʈ�� Ȱ��ȭ�ǰ�,
            //gameObject.SetActive(true);

            // ���� ȭ�� ������ ������ ������� ���ƿͼ�, 
            // ������ ��ġ�� ���� ���� ����.
            transform.position = Vector3.Slerp(transform.position, bossPosition, 0.03f);

        }

        // ���� ȭ������ �ɾ���ͼ�
        // �÷��̾��� ������ ������ ü���� �پ���. 
    }
}
