using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : MonoBehaviour
{
    // �ӷ� ����
    public float speed = 5;

    // �� óġ ��
    public static int enemyDeath = 0;

    // �� �ִ� óġ ��
    public static int maxEnemyDeath = 10;

    // ���ݷ� ����
    public int attackPower = 2;

    // PlayerMove ������Ʈ ����
    PlayerMove_Slime pms;

    void Start()
    {
        pms = GetComponent<PlayerMove_Slime>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ�
        if (GameManager_Slime.gm.gState != GameManager_Slime.GameState.Run)
        {
            return;
        }

        // *�������� ��� �̵��ϱ�
        // 1. �������� ������ �����
        Vector3 dir = Vector3.left;
        // 2. �̵��Ѵ�.
        transform.position += dir * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 1) * 135 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ٸ� ��ü�� �ε����ٸ� Enemy�� �״´�.
        Destroy(this.gameObject);

        // ���� �÷��̾�� �ε�����, 
        // �÷��̾��� ü���� 2��ŭ ���δ�.
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove_Slime>().OnDamage(attackPower);
        }
    }
}
