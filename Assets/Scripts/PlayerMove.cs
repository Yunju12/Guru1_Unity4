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

    public bool isDelay;
    public float delayTime = 3.0f;
    public float currentTime;

    // �ִϸ��̼� ����
    Animator ani;

    // ����� �ҽ� ������Ʈ
    private AudioSource audio_pm;

    // ����� Ŭ�� ����
    public AudioClip clip;

    public AudioClip hurt;

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

        // 4. �÷��̾� ȭ�� ������ �������� �Ѵ�.
        // ���� �÷��̾��� ���� ��ǥ(transform.position)�� ����Ʈ ���� ��ǥ�� ��ȯ�Ѵ�.
        Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);

        // �Էµ� ���� 0~1 ���̸� ����� ���ϰ� ������ �����Ѵ�.
        viewPosition.x = Mathf.Clamp01(viewPosition.x);
        viewPosition.y = Mathf.Clamp01(viewPosition.y);

        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewPosition);
        transform.position = worldPosition;

        // * ����
        // ���� ���� Ű�� �����ٸ�,
        // (��, ���� Ƚ���� �ִ� ���� Ƚ���� �Ѿ�� �ʾҾ�� �Ѵ�.)
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            // ���� ����� Ŭ���� �ѹ� �����Ѵ�.
            audio_pm.PlayOneShot(clip);

            // ���� �ӵ��� �������� �����ϰ� jumpCount�� 1��ŭ �ö󰣴�.
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            // Jump �ִϸ��̼��� �����Ѵ�.
            ani.SetTrigger("ToJump");
        }
        // ���� �¿� ����Ű�� �����ٸ�, Move �ִϸ��̼��� �����Ѵ�.
        else if (Input.GetButtonDown("Horizontal"))
        {
            ani.SetTrigger("JumpToMove");
        }
        // �� �� �ƴ϶��, Idle �ִϸ��̼��� �����Ѵ�.
        else
        {
            ani.SetTrigger("JumpToIdle");
        }

        // ���� �����̽��ٸ� ������ �ִٸ�, ������ ����ؼ� �ö� �� �ְ� �Ѵ�.
        if (Input.GetButton("Jump"))
        {
            GetComponent<CircleCollider2D>().isTrigger = true; 
        }

        // ���� �÷��̾��� �ӵ��� 0 ���� �۴ٸ�(�Ʒ��� �������� ���̶��), ������ ����� �� ���� �Ѵ�.
        if (rigid.velocity.y < 0)
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

    // * �÷��̾� �ǰ� �Լ�
    // �÷��̾ ���� ������ �޾��� ��
    public void OnDamage(int value)
    {
        // hurt ����� Ŭ���� �ѹ� �����Ѵ�.
        audio_pm.PlayOneShot(hurt);

        // ü���� �پ��� �Ѵ�.
        playerHp -= value;
        
        // Hurt �ִϸ��̼��� �����ϰ� �⺻ �ִϸ��̼����� ���ư���.
        ani.SetTrigger("ToHurt");
        ani.SetTrigger("Exit");

        // ���� ü���� 0 �� ���ų� �׺��� �۴ٸ�, ü���� ���� 0 ���� �����Ѵ�.
        if (playerHp <= 0)
        {
            playerHp = 0;
        }
    }

    // * �÷��̾� ��� �Լ�
    // �÷��̾ ������ Die �ִϸ��̼��� �����Ѵ�.
    public void Die()
    {
        ani.SetTrigger("ToDie");
    }
}
