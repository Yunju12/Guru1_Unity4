using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Slime : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 10;

    // ���ݷ�
    public int attackPower = 2;

    BossMonster_Slime bms;

    void Start()
    {
        bms = GetComponent<BossMonster_Slime>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager_Slime.gm.gState == GameManager_Slime.GameState.GameOver)
        {
            this.gameObject.SetActive(false);
        }

        // ���������� ��� �̵��ϱ�
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ٸ� ��ü�� �ε����ٸ� Bullet�� �������.
        Destroy(this.gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy_Slime.enemyDeath++;
            GameObject.FindWithTag("GameManager").GetComponent<GameManager_Slime>().totalPoint += 50;
        }

        else if (collision.gameObject.tag == "BossMonster")
        {
            collision.gameObject.GetComponent<BossMonster_Slime>().BossOnDamage(attackPower);
        }
    }
}
