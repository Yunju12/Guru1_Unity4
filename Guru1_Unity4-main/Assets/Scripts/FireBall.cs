using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 10;

    // ���� ����
    Vector3 dir;

    // ���ݷ� ����
    public int attackPower = 2;

    void Start()
    {
  
    }

    void Update()
    {
        // ���� ���°� Run �� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ������ �������� ��� �̵��Ѵ�.
        dir = Vector3.left;
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
