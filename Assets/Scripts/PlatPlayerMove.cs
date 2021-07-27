using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatPlayerMove : MonoBehaviour
{
    // ���� �Ŵ��� ����
    public GameManager GameManager;

    // �ִ�ӵ� ����
    public float maxSpeed;

    //������ ����
    public float jumpPower;

    // �ִ� ���� Ƚ��
    public int maxJump = 2;

    // ���� ���� Ƚ��
    int jumpCount = 0;

    Rigidbody2D rigid; 
    SpriteRenderer spriteRenderer;

    
    // �ִϸ�����
    Animator anim; 


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
        if (Input.GetButton("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            anim.GetBool("isJumping");
            anim.SetBool("isJumping", true);

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                jumpCount = 0;
            }
        }

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
           // rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            //Ű�� ����,x�� �ӵ� �⺻ 0.5��, y�� �ӵ��� �״��
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            
            //Ű�� ������ ������, ���ʴ����� -1�Ǽ� �¿�ٲٱ�
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

    void FixedUpdate()
    {
        // * �̵��ӵ� ����
        float h = Input.GetAxisRaw("Horizontal");       //h�� Ű�� ������ �Է� ������=1,����=-1
        rigid.AddForce(Vector2.right * h*2, ForceMode2D.Impulse); //h * �����ʰ��ؼ� ���� ��

        // * �̵��ӵ� ����
        // ������ �ӵ� ����
        if (rigid.velocity.x > maxSpeed)         //x�ӵ��� maxSpeed ���� ũ��, �ӵ� maxSpeed�� ����
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        // ���� �ӵ� ����
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
                    anim.SetBool("isJumping", false);
                }
            }
        }
        
    }

    // ���� �浹
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // ���� : ���� �� + ������ = ����
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }

            // ������ �Լ� ȣ��
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
            // ����Ʈ

            // ������ �����
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "Finish")
        {
            // ���� ����������!

        }
    }

    void OnAttack(Transform PlatEnemy)
    {
        //

        rigid.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        //
        PlatEnemyMove platEnemyMove = PlatEnemy.GetComponent<PlatEnemyMove>();
        platEnemyMove.OnDamaged();
    }

    // �浹�� �������� ����
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

