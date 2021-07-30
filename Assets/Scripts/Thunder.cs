using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 10;

    // ���� ����
    Vector3 dir;

    // ���ݷ� ����
    public int attackPower = 2;

    // 
    PlayerMove_Slime pms;

    void Start()
    {
        pms = GetComponent<PlayerMove_Slime>();
    }

    void Update()
    {
        // ���� ���°� Run �� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager_Slime.gm.gState != GameManager_Slime.GameState.Run)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ٸ� ��ü�� �ε����ٸ� FireBall�� �������.
        Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerMove_Slime>().OnDamage(attackPower);
        }
    }
}
