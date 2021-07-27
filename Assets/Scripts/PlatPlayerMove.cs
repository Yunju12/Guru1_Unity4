using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatPlayerMove : MonoBehaviour
{
    // ���� �Ŵ��� ����
    public PlatGameManager PlatGameManager;

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

    CapsuleCollider2D capsuleCollider;

    // �ִϸ�����
    Animator anim; 


    // ���۽� �ʱ�ȭ
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    //rigid ���� �ʱ�ȭ
        maxSpeed = 5f;              //�ִ�ӵ�
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

    // ����ȿ�� �������� �����ϰ� ȣ���ϴ� FixedUpdate ���
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


        // Floor ����
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); // ���, �Ʒ����� Ray ǥ��
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
            bool isStar = collision.gameObject.name.Contains("ItemStar");
            bool isCarrot = collision.gameObject.name.Contains("ItemCarrot");

            if (isStar)
                PlatGameManager.stagePoint += 100;
            else if (isCarrot)
                PlatGameManager.stagePoint += 30;
            // ������ �����
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Finish")
        {
            // ���� ����������!
            PlatGameManager.NextStage();
        }
    }

    // ���ݽ�
    void OnAttack(Transform PlatEnemy)
    {
        // ����Ʈ
        PlatGameManager.stagePoint += 100;

        // Enemy ���� ��� ���� �ݵ�
        rigid.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        // Enemy ����
        PlatEnemyMove platEnemyMove = PlatEnemy.GetComponent<PlatEnemyMove>();
        platEnemyMove.OnDamaged();
    }

    // �ǰݽ�
    void OnDamaged(Vector2 targetPos)
    {
        // Hp���� ȣ�� : 1�� ����
        PlatGameManager.HpDown();

        // player ���̾� �ٲ� (Player -> PlaterDamaged ��!)
        gameObject.layer = 12;

        // �浹�� �������� ����( player �������ϰ� ����)
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // ������ ���� ��� �ݵ� ��
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*5, ForceMode2D.Impulse);

        // �ִϸ��̼� ����
        anim.SetTrigger("doDamaged"); // ������ �ִϸ��̼� ���
        Invoke("OffDamaged", 3); // 3�ʰ� �������� ����
    }

    // �ǰݽ� �������� -> ���� ���·� ���ƿ�
    void OffDamaged()
    {
        // player ���̾� ������ (PlayerDamaged -> Plater ��!)
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1); // ���� ������ ����

    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        capsuleCollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    }    
}

