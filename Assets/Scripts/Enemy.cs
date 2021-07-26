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

    // PlayerMove ������Ʈ ����
    PlayerMove pm;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ�
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // *�������� ��� �̵��ϱ�
        // 1. �������� ������ �����
        Vector3 dir = Vector3.left;
        // 2. �̵��Ѵ�.
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ���� �÷��̾�� �ε�����, 
        // �÷��̾��� ü���� 2��ŭ ���δ�.
        if (collider.gameObject.CompareTag("Player"))
        {
            pm.OnDamage(attackPower);
        }

        // �ٸ� ��ü�� �ε����ٸ� Enemy�� �״´�.
        Destroy(this.gameObject);
    }
}
