using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatPlayerMove : MonoBehaviour
{
    // 게임 매니저 변수
    public PlatGameManager PlatGameManager;

    // 최대속도 변수
    public float maxSpeed;

    //점프력 변수
    public float jumpPower;

    // 최대 점프 횟수
    public int maxJump = 2;

    // 현재 점프 횟수
    int jumpCount = 0;

    Rigidbody2D rigid; 
    SpriteRenderer spriteRenderer;

    CapsuleCollider2D capsuleCollider;

    // 애니메이터
    Animator anim; 


    // 시작시 초기화
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    //rigid 변수 초기화
        maxSpeed = 5f;              //최대속도
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {
        // Jump
        if (Input.GetButton("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            anim.GetBool("isJumping");
            anim.SetBool("isJumping", true);

        }

       /* void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                jumpCount = 0;
            }
        }
        */

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
           // rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            //키를 떼면,x축 속도 기본 0.5배, y축 속도는 그대로
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            
            //키를 누르고 있으면, 왼쪽누르면 -1되서 좌우바꾸기
        }

        // Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }

        else
        {
            anim.SetBool("isWalking", true);
        }
    }

    // 물리효과 적용위해 일정하게 호출하는 FixedUpdate 사용
    void FixedUpdate()
    {
        // * 이동속도 조절
        float h = Input.GetAxisRaw("Horizontal");       //h에 키를 누르면 입력 오른쪽=1,왼쪽=-1
        rigid.AddForce(Vector2.right * h*2, ForceMode2D.Impulse); //h * 오른쪽곱해서 힘을 줌

        // * 이동속도 제한
        // 오른쪽 속도 제한
        if (rigid.velocity.x > maxSpeed)         //x속도가 maxSpeed 보다 크면, 속도 maxSpeed로 고정
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        // 왼쪽 속도 제한
        else if (rigid.velocity.x < maxSpeed * (-1))       //x속도가 -maxSpeed 보다 작으면(왼쪽으로 갈때) 속도는 -maxSpeed로 고정
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        // Floor 착지
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); // 녹색, 아래방향 Ray 표시
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Floor"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
        
    }

    // 적과 충돌
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 공격 : 몬스터 위 + 낙하중 = 밟음
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }

            // 데미지 함수 호출
            else
            {
                OnDamaged(collision.transform.position);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            // 포인트
            bool isStar = collision.gameObject.name.Contains("ItemStar");
            bool isCarrot = collision.gameObject.name.Contains("ItemCarrot");

            if (isStar)
                PlatGameManager.stagePoint += 100;
            else if (isCarrot)
                PlatGameManager.stagePoint += 30;
            // 아이템 사라짐
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Finish")
        {
            // 다음 스테이지로!
            PlatGameManager.NextStage();
        }
    }

    // 공격시
    void OnAttack(Transform PlatEnemy)
    {
        // 포인트
        PlatGameManager.stagePoint += 100;

        // Enemy 밟을 경우 위로 반동
        rigid.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        // Enemy 죽음
        PlatEnemyMove platEnemyMove = PlatEnemy.GetComponent<PlatEnemyMove>();
        platEnemyMove.OnDamaged();
    }

    // 피격시
    void OnDamaged(Vector2 targetPos)
    {
        // Hp감소 호출 : 1씩 감소
        PlatGameManager.HpDown();

        // player 레이어 바꿈 (Player -> PlaterDamaged 로!)
        gameObject.layer = 12;

        // 충돌시 무적상태 만듦( player 불투명하게 보임)
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 데미지 입을 경우 반동 줌
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*5, ForceMode2D.Impulse);

        // 애니메이션 설정
        anim.SetTrigger("doDamaged"); // 데미지 애니메이션 출력
        Invoke("OffDamaged", 3); // 3초간 무적상태 설정
    }

    // 피격시 무적상태 -> 원래 상태로 돌아옴
    void OffDamaged()
    {
        // player 레이어 원상태 (PlayerDamaged -> Plater 로!)
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1); // 원래 색으로 변경

    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        capsuleCollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    }    
}

