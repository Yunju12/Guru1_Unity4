using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӷ� ����
    public float speed = 5;

    // Enemy ���� ��
    public static int enemyDeath = 0;

    // �÷��̾� ����
    GameObject player;

    // ���ݷ� ����
    public int attackPower = 2;


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
        if (collision.gameObject.CompareTag("Floor"))
            return;

        // 2. ���� �÷��̾�� �ε��ϸ�, 
        // �÷��̾�� ü���� 2��ŭ ���̰�, �� ���� ���� �ϳ� �ö󰣴�.
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();
            pm.OnDamage(attackPower);
        }

        // (���� �ƴ�)�ٸ� ��ü�� �ε����ٸ� Enemy�� �״´�.
        Destroy(gameObject);
    }
}
