using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 10;

    // ���ݷ�
    public int attackPower = 2;

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

        // * �������� ��� �̵��ϱ�
        // 1. �������� ������ �����
        Vector3 dir = Vector3.left;
        // 2. �̵��Ѵ�.
        transform.position += dir * speed * Time.deltaTime;
    }

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
