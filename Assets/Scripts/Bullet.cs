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
        // ���������� ��� �̵��ϱ�
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Enemy.enemyDeath++;
        }

        else if (collider.gameObject.tag == "BossMonster")
        {
            BossMonster bm = GameObject.Find("BossMonster").GetComponent<BossMonster>();
            bm.BossOnDamage(attackPower);
        }

        // �ٸ� ��ü�� �ε����ٸ� Bullet�� �������.
        Destroy(this.gameObject);
    }
}
