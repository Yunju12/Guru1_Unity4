using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // 캐릭터를 좌우로 움직이게 하고 점프를 하게 하고싶다.

    // 중력 변수
    public float gravity = -20.0f;

    // 점프력 변수
    public float jumpPower = 10.0f;

    // 최대 점프 횟수
    public int maxJump = 2;

    // 현재 점프 횟수
    int jumpCount = 0;

    // 수직 속도 변수
    float yVelocity = 0;

    // 속력 변수
    public float movespeed = 7.0f;

    // 캐릭터 컨트롤러 변수
    CharacterController cc;

    // 체력 변수
    public static int hp;

    // 최대 체력 변수
    public int maxHp = 10;

    // 슬라이더 바
    public Slider hpSlider;

    void Start()
    {
        // 캐릭터 컨트롤러 컴포넌트를 받아온다.
        cc = GetComponent<CharacterController>();

        // 체력 변수 초기화
        hp = maxHp;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        
        // 이동 방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, 0);
        dir.Normalize();

        // 만일 플레이어가 땅에 착지하였다면 현재 점프 횟수를 0으로 초기화한다.
        // 수직 속도 값(중력)을 다시 0으로 초기화한다.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        // 만일 점프 키를 누른다면, 점프력을 수직 속도로 적용한다.
        // 단, 현재 점프 횟수가 최대 점프 횟수를 넘어가지 않았어야 한다.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        // 캐릭터의 수직속도(중력)을 적용한다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 이동 방향으로 플레이어를 이동시킨다.
        cc.Move(dir * movespeed * Time.deltaTime);

        // 슬라이더의 value를 체력 비율로 적용한다.
        hpSlider.value = (float)hp / (float)maxHp;

        // 체력 바 이동
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
