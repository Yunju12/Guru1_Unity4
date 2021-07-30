using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 10;

    // ���ݷ�
    public int attackPower = 2;

    // ���� ���� ������Ʈ ����
    BossMonster bm;

    public GameManager gm;

    void Start()
    {
        bm = GetComponent<BossMonster>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState == GameManager.GameState.GameOver)
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
            Enemy.enemyDeath++;
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().totalPoint += 50;
        }

        else if (collision.gameObject.tag == "BossMonster")
        {
            
            collision.gameObject.GetComponent<BossMonster>().BossOnDamage(attackPower);
        }
    }
}
