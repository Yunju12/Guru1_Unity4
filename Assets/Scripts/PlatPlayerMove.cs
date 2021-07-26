using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatPlayerMove : MonoBehaviour
{
    public float maxSpeed;          //�ִ�ӵ����� ����
    public float jumpPower;
    Rigidbody2D rigid;  //Rigidbody2D -������ rigid ���� 
    SpriteRenderer spriteRenderer;
    Animator anim;
    int jumpCount;

    void Awake() // �ʱ�ȭ
    {
        rigid = GetComponent<Rigidbody2D>();    //rigid ���� �ʱ�ȭ
        maxSpeed = 5f;              //�ִ�ӵ�
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
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
          
            //Ű�� ����,x�� �ӵ� �⺻ 0.5��, y�� �ӵ��� �״��
        }

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            
            //Ű�� ������ ������, ���ʴ����� -1�Ǽ� �¿�ٲٱ�
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
        float h = Input.GetAxisRaw("Horizontal");       //h�� Ű�� ������ �Է� ������=1,����=-1
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //h * �����ʰ��ؼ� ���� ��

        // Max Speed
        if (rigid.velocity.x > maxSpeed)         //x�ӵ��� maxSpeed ���� ũ��, �ӵ� maxSpeed�� ����
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))       //x�ӵ��� -maxSpeed ���� ������(�������� ����) �ӵ��� -maxSpeed�� ����
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
    
}