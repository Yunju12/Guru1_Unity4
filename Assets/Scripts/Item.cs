using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // �ӷ� ����
    public float speed = 5;

    void Start()
    {

    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ�
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        Vector3 speed = new Vector3(5, 5, 0);
        GetComponent<Rigidbody2D>().AddForce(speed);
    }
}