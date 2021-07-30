using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // �̵��� ���� ����
    Rigidbody2D rigid;

    // ������ ����
    public float jumpPower = 10.0f;

    // �ִ� ���� Ƚ��
    public int maxJump = 2;

    // ���� ���� Ƚ��
    int jumpCount = 0;

    // �ӷ� ����
    public float moveSpeed = 7.0f;

    // ü�� ����
    public static int playerHp;

    // �ִ� ü�� ����
    public int maxHp = 10;

    // �����̴� ��
    public Slider hpSlider;

    // ������ ȿ�� ���� �ð� ����
    public float itemTime = 5;

    // �ִϸ��̼� ����
    Animator ani;

    // ����� �ҽ� ������Ʈ
    private AudioSource audio_pm;

    // ����� Ŭ�� ����
    public AudioClip clip;

    public AudioClip hurt;

    Item item;

    Bullet bul;

    // �÷��̾� �ִϸ��̼� ���
    public enum PlayerState
    {
        Idle,
        Move,
        Jump,
        Die
    }

    void Start()
    {
        // ü�� ���� �ʱ�ȭ
        playerHp = maxHp;

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        rigid = GetComponent<Rigidbody2D>();

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        audio_pm = GetComponent<AudioSource>();
    }

    void Update()
    {
        // HP �ٰ� ĳ������ �Ӹ� ���� ��� ��ġ�Ѵ�.
        hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));

        // ���� ���°� ���� ���� ���°� �Ǹ� Die �ִϸ��̼��� �����Ѵ�.
        if (playerHp <= 0)
        {
            ani.SetTrigger("ToDie");
        }

        // * HP ��
        // �����̴��� value�� ü�� ������ �����Ѵ�.
        hpSlider.value = (float)playerHp / (float)maxHp;

        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // * �̵�
        // 1. �̵� ����(�¿�)�� �����Ѵ�.
        float h = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(h, 0, 0);
        dir.Normalize();

        // 2. �̵� ����(�¿�)���� �÷��̾ �̵���Ų��.
        transform.position += (dir * moveSpeed * Time.deltaTime);

        // 3. �����̸� IdleToMove, �������� ���߸� MoveToIdle �� �����Ѵ�.
        if (Input.GetButtonDown("Horizontal"))
        {
            ani.SetTrigger("IdleToMove");
        }
        else
        {
            ani.SetTrigger("MoveToIdle");
        }

        // * ����
        // ���� ���� Ű�� �����ٸ�,
        // (��, ���� Ƚ���� �ִ� ���� Ƚ���� �Ѿ�� �ʾҾ�� �Ѵ�.)
        // ���� �ӵ��� �������� �����ϰ� jumpCount�� 1��ŭ �ö󰣴�.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            audio_pm.PlayOneShot(clip);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            // �ִϸ��̼�
            ani.SetTrigger("ToJump");
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            ani.SetTrigger("JumpToMove");
        }
        else
        {
            ani.SetTrigger("JumpToIdle");
        }

        if (Input.GetButton("Jump"))
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    // ���� �÷��̾ ���� �����Ͽ��ٸ�,
    // ���� ���� Ƚ���� 0���� �ʱ�ȭ�Ѵ�.
    // ���� �ִϸ��̼��� �����Ѵ�.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            /* ü�� ȸ��
            if (item.item == item.i1)
            {
                playerHp += 5;
            }

            // ���� ����
            else if (item.item == item.i2)
            {
                gameObject.layer = 12;

                Invoke("Off", itemTime);
            }*/

            // ���ݷ� �ι�
            Invoke("PowerUp", itemTime);
        }
    }

    void Off()
    {
        gameObject.layer = 6;
    }


    void PowerUp()
    {
        bul.attackPower = 4;
    }

    // * �÷��̾� �ǰ� �Լ�
    // �÷��̾ ���� ������ �޾��� �� ü���� �پ�鵵�� �Ѵ�.
    // �÷��̾��� ü���� 0���ϰ� �Ǹ� ü�� ������ ���� 0���� �����Ѵ�.
    public void OnDamage(int value)
    {

        audio_pm.PlayOneShot(hurt);

        playerHp -= value;
        
        ani.SetTrigger("ToHurt");
        ani.SetTrigger("Exit");

        if (playerHp <= 0)
        {
            playerHp = 0;
        }
    }

    public void Die()
    {
        ani.SetTrigger("ToDie");
    }
}
