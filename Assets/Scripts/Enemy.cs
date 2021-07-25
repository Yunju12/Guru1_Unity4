using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӷ� ����
    public float speed = 5;

    // Enemy ���� ��
    public static int enemyDeath = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // *�������� ��� �̵��ϱ�
        // 1. �������� ������ �����
        Vector3 dir = Vector3.left;
        // 2. �̵��Ѵ�.
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // *�ٸ� ������Ʈ�� �ε����� ��
        // 1. ���� ���� ��ü�� �ٴ��̶�� �����ϰ�
        if (collision.gameObject.tag == "Floor")
            return;
        // �ٸ� ��ü��� Enemy�� �״´�.
        Destroy(gameObject);
        // 2. ���� �÷��̾�� �ε��ϸ�, 
        // �÷��̾�� ü���� 1��ŭ ���̰�, �� ���� ���� �ϳ� �ö󰣴�.
        if (collision.gameObject.tag == "Player")
        {
            PlayerMove.hp = PlayerMove.hp - 1;
            enemyDeath++;
        }
    }
}
