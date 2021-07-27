using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // 이동을 위한 변수
    Rigidbody2D rigid;

    // 점프력 변수
    public float jumpPower = 10.0f;

    // 최대 점프 횟수
    public int maxJump = 2;

    // 현재 점프 횟수
    int jumpCount = 0;

    // 속력 변수
    public float moveSpeed = 7.0f;

    // 체력 변수
    public static int playerHp;

    // 최대 체력 변수
    public int maxHp = 10;

    // 슬라이더 바
    public Slider hpSlider;

    // 애니메이션 변수
    Animator ani;

    GameManager gm;

    // 플레이어 애니메이션 상수
    public enum PlayerState
    {
        Idle,
        Move,
        Jump,
        Die
    }

    void Start()
    {
        // 체력 변수 초기화
        playerHp = maxHp;

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        rigid = GetComponent<Rigidbody2D>();

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();

        gm = GetComponent<GameManager>();
    }

    void Update()
    {
        // HP 바가 캐릭터의 머리 위에 계속 위치한다.
        hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));

        // 게임 상태가 게임 오버 상태가 되면 Die 애니메이션을 실행한다.
        if (playerHp <= 0)
        {
            ani.SetTrigger("ToDie");
        }

        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

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

        // * 점프
        // 만일 점프 키를 누른다면,
        // (단, 점프 횟수가 최대 점프 횟수를 넘어가지 않았어야 한다.)
        // 수직 속도로 점프력을 적용하고 jumpCount가 1만큼 올라간다.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            // 애니메이션
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

        // * HP 바
        // 슬라이더의 value를 체력 비율로 적용한다.
        hpSlider.value = (float)playerHp / (float)maxHp;

        
    }

    // 만일 플레이어가 땅에 착지하였다면,
    // 현재 점프 횟수를 0으로 초기화한다.
    // 점프 애니메이션을 종료한다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }
    }

    // * 플레이어 피격 함수
    // 플레이어가 적의 공격을 받았을 때 체력이 줄어들도록 한다.
    // 플레이어의 체력이 0이하가 되면 체력 변수의 값을 0으로 고정한다.
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

            // 게임 상태를 게임 오버 상태로 전환한다.
            gm.gState = GameManager.GameState.GameOver;
        }
    }
}
