using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatPlayerMove : MonoBehaviour
{
    public float maxSpeed;          //최대속도변수 선언
    public float jumpPower;
    Rigidbody2D rigid;  //Rigidbody2D -변수명 rigid 선언 
    SpriteRenderer spriteRenderer;
    Animator anim;
    int jumpCount;

    void Awake() // 초기화
    {
        rigid = GetComponent<Rigidbody2D>();    //rigid 변수 초기화
        maxSpeed = 5f;              //최대속도
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Jump
        if (Input.GetButton("Jump") && anim.GetBool("isPlatJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isPlatJumping", true);
            
        }

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
            anim.SetBool("isPlatWalking", false);
        }

        else
        {
            anim.SetBool("isPlatWalking", true);
        }
    }

    void FixedUpdate()
    {
        // Move Speed
        float h = Input.GetAxisRaw("Horizontal");       //h에 키를 누르면 입력 오른쪽=1,왼쪽=-1
        rigid.AddForce(Vector2.right * h*2, ForceMode2D.Impulse); //h * 오른쪽곱해서 힘을 줌

        // Max Speed
        if (rigid.velocity.x > maxSpeed)         //x속도가 maxSpeed 보다 크면, 속도 maxSpeed로 고정
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))       //x속도가 -maxSpeed 보다 작으면(왼쪽으로 갈때) 속도는 -maxSpeed로 고정
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        // Landing Platform
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Floor"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isPlatJumping", false);
                }
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position); 
        }
    }

    // 충돌시 무적상태 만듦
    void OnDamaged(Vector2 targetPos)
    {
        // Change Layer (Immortal Active)
        gameObject.layer = 12;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*5, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("doDamaged");


        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;

        spriteRenderer.color = new Color(1, 1, 1, 1);

    }
}