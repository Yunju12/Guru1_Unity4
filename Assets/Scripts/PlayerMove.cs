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

    // 아이템 효과 유지 시간 변수
    public float itemTime = 5;

    public bool isDelay;
    public float delayTime = 3.0f;
    public float currentTime;

    // 애니메이션 변수
    Animator ani;

    // 오디오 소스 컴포넌트
    private AudioSource audio_pm;

    // 오디오 클립 변수
    public AudioClip clip;

    public AudioClip hurt;

    Bullet bul;

    

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

        audio_pm = GetComponent<AudioSource>();
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

        // * HP 바
        // 슬라이더의 value를 체력 비율로 적용한다.
        hpSlider.value = (float)playerHp / (float)maxHp;

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

        // 4. 플레이어 화면 밖으로 못나가게 한다.
        // 현재 플레이어의 월드 좌표(transform.position)을 뷰포트 기준 좌표로 변환한다.
        Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);

        // 입력된 값이 0~1 사이를 벗어나지 못하게 강제로 조정한다.
        viewPosition.x = Mathf.Clamp01(viewPosition.x);
        viewPosition.y = Mathf.Clamp01(viewPosition.y);

        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewPosition);
        transform.position = worldPosition;

        // * 점프
        // 만일 점프 키를 누른다면,
        // (단, 점프 횟수가 최대 점프 횟수를 넘어가지 않았어야 한다.)
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            // 점프 오디오 클립을 한번 실행한다.
            audio_pm.PlayOneShot(clip);

            // 수직 속도로 점프력을 적용하고 jumpCount가 1만큼 올라간다.
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            // Jump 애니메이션을 실행한다.
            ani.SetTrigger("ToJump");
        }
        // 만약 좌우 방향키를 누른다면, Move 애니메이션을 실행한다.
        else if (Input.GetButtonDown("Horizontal"))
        {
            ani.SetTrigger("JumpToMove");
        }
        // 둘 다 아니라면, Idle 애니메이션을 실행한다.
        else
        {
            ani.SetTrigger("JumpToIdle");
        }

        // 만약 스페이스바를 누르고 있다면, 발판을 통과해서 올라갈 수 있게 한다.
        if (Input.GetButton("Jump"))
        {
            GetComponent<CircleCollider2D>().isTrigger = true; 
        }

        // 만약 플레이어의 속도가 0 보다 작다면(아래로 떨어지는 중이라면), 발판을 통과할 수 없게 한다.
        if (rigid.velocity.y < 0)
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
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
    // 플레이어가 적의 공격을 받았을 때
    public void OnDamage(int value)
    {
        // hurt 오디오 클립을 한번 실행한다.
        audio_pm.PlayOneShot(hurt);

        // 체력이 줄어들게 한다.
        playerHp -= value;
        
        // Hurt 애니메이션을 실행하고 기본 애니메이션으로 돌아간다.
        ani.SetTrigger("ToHurt");
        ani.SetTrigger("Exit");

        // 만약 체력이 0 과 같거나 그보다 작다면, 체력의 값을 0 으로 고정한다.
        if (playerHp <= 0)
        {
            playerHp = 0;
        }
    }

    // * 플레이어 사망 함수
    // 플레이어가 죽으면 Die 애니메이션을 실행한다.
    public void Die()
    {
        ani.SetTrigger("ToDie");
    }
}
