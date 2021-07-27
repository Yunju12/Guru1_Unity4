using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_PlayerMove : MonoBehaviour
{
    // 이동을 위한 변수
    Rigidbody2D rigid;

    // 속력 변수
    public float moveSpeed = 7.0f;

    // 애니메이션 변수
    Animator ani;

    GameManager gm;

    // 오디오 소스 컴포넌트
    private AudioSource audio;

    public enum PlayerState
    {
        Idle,
        Move
    }

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 애니메이션 컴포넌트를 받아온다.
        rigid = GetComponent<Rigidbody2D>();

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();

        gm = GetComponent<GameManager>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // * 이동
        // 1. 이동 방향(좌우)을 설정한다.
        float h = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(h, 0, 0);
        dir.Normalize();

        // 2. 이동 방향(좌우)으로 플레이어를 이동시킨다.
        transform.position += (dir * moveSpeed * Time.deltaTime);

        // 3. 움직이면 IdleToMove, 움직임을 멈추면 MoveToIdle 을 실행한다.
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
