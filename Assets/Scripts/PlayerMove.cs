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

    // �ִϸ��̼� ����
    Animator ani;

    GameManager gm;

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

        gm = GetComponent<GameManager>();
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

        // * HP ��
        // �����̴��� value�� ü�� ������ �����Ѵ�.
        hpSlider.value = (float)playerHp / (float)maxHp;

        
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

    // * �÷��̾� �ǰ� �Լ�
    // �÷��̾ ���� ������ �޾��� �� ü���� �پ�鵵�� �Ѵ�.
    // �÷��̾��� ü���� 0���ϰ� �Ǹ� ü�� ������ ���� 0���� �����Ѵ�.
    public void OnDamage(int value)
    {
        Debug.Log("Damage");

        playerHp -= value;
        ani.SetTrigger("ToHurt");
        ani.SetTrigger("Exit");

        if (playerHp < 0)
        {
            playerHp = 0;
            ani.SetTrigger("ToDie");

            // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
            gm.gState = GameManager.GameState.GameOver;
        }
    }
}
