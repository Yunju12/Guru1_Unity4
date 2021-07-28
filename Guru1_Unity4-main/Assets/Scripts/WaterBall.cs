using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 10;

    // ���� ����
    Vector3 dir;

    // ���ݷ� ����
    public int attackPower = 2;

    void Start()
    {

        // * ������ �� 30% Ȯ���� �÷��̾� ����, ������ Ȯ���� ���� �������� ���� �ϱ�
        // 1. �������� 0 ���� 9 �� �ϳ��� �����Ѵ�.
        int randomValue = Random.Range(0, 10);

        // 2. ���� �������� 0�̸�
        if (randomValue == 0)
        {
            // �÷��̾��� ��ġ�� ã��,
            GameObject target = GameObject.Find("Player");

            // �÷��̾��� ���� ������ ���ư���.
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        // 3. �׷��� ������ �������� ���ư���.
        else
        {
            dir = Vector3.left;
        }
            
    }

    void Update()
    {
        // ���� ���°� Run �� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ������ �������� ��� �̵��Ѵ�.
        transform.position += dir * speed * Time.deltaTime;
    }

    // * ����
    // ���� �÷��̾�� �ε�����
    // �÷��̾��� ü���� 2��ŭ ���δ�.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // �ٸ� ��ü�� �ε����ٸ� FireBall�� �������.
        Destroy(this.gameObject);

        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();
            pm.OnDamage(attackPower);
        }
    }
}
