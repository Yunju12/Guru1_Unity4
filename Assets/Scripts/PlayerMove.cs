using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // ĳ���͸� �¿�� �����̰� �ϰ� ������ �ϰ� �ϰ�ʹ�.

    // �߷� ����
    public float gravity = -20.0f;

    // ������ ����
    public float jumpPower = 10.0f;

    // �ִ� ���� Ƚ��
    public int maxJump = 2;

    // ���� ���� Ƚ��
    int jumpCount = 0;

    // ���� �ӵ� ����
    float yVelocity = 0;

    // �ӷ� ����
    public float movespeed = 7.0f;

    // ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;

    // ü�� ����
    public static int hp;

    // �ִ� ü�� ����
    public int maxHp = 10;

    // �����̴� ��
    public Slider hpSlider;

    void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ�� �޾ƿ´�.
        cc = GetComponent<CharacterController>();

        // ü�� ���� �ʱ�ȭ
        hp = maxHp;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        
        // �̵� ������ �����Ѵ�.
        Vector3 dir = new Vector3(h, 0, 0);
        dir.Normalize();

        // ���� �÷��̾ ���� �����Ͽ��ٸ� ���� ���� Ƚ���� 0���� �ʱ�ȭ�Ѵ�.
        // ���� �ӵ� ��(�߷�)�� �ٽ� 0���� �ʱ�ȭ�Ѵ�.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        // ���� ���� Ű�� �����ٸ�, �������� ���� �ӵ��� �����Ѵ�.
        // ��, ���� ���� Ƚ���� �ִ� ���� Ƚ���� �Ѿ�� �ʾҾ�� �Ѵ�.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        // ĳ������ �����ӵ�(�߷�)�� �����Ѵ�.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // �̵� �������� �÷��̾ �̵���Ų��.
        cc.Move(dir * movespeed * Time.deltaTime);

        // �����̴��� value�� ü�� ������ �����Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;

        // ü�� �� �̵�
        hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    }

    public void OnDamage(int value)
    {
        
        if (hp < 0)
        {
            hp = 0;
        }
    }
}
