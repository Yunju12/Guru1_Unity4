using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_PlayerMove : MonoBehaviour
{
    // �̵��� ���� ����
    Rigidbody2D rigid;

    // �ӷ� ����
    public float moveSpeed = 7.0f;

    // �ִϸ��̼� ����
    Animator ani;

    GameManager gm;

    // ����� �ҽ� ������Ʈ
    private AudioSource audio;

    public enum PlayerState
    {
        Idle,
        Move
    }

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        rigid = GetComponent<Rigidbody2D>();

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        gm = GetComponent<GameManager>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
