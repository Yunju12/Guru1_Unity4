using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        // ������ ��� �̵��ϱ�
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy.enemyDeath++;
        }

        else if (collision.gameObject.tag == "BossMonster")
        {
            BossMonster bm = GameObject.Find("BossMonster").GetComponent<BossMonster>();
            bm.BossOnDamage(attackPower);
        }

        // (���� �ƴ�)�ٸ� ��ü�� �ε����ٸ� Bullet�� �������.
        Destroy(gameObject);
    }
}
