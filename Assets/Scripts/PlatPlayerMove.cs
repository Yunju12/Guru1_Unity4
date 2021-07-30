using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatPlayerMove : MonoBehaviour
{
    // ���� �Ŵ��� ����
    public PlatGameManager PlatGameManager;

    // ����� �ҽ� ������Ʈ
    AudioSource audioSource;

    // ����� Ŭ�� ����
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    // �ִ�ӵ� ����
    public float maxSpeed;

    //������ ����
    public float jumpPower;

    // �ִ� ���� Ƚ��
    public int maxJump = 2;

    // ���� ���� Ƚ��
    int jumpCount = 0;

    // �����̵� ����
    Rigidbody2D rigid;

    SpriteRenderer spriteRenderer;

    // ���� �浹 ���
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
        audioSource = GetComponent<AudioSource>();
    }

   

    void Update()
    {
        // ����
        if (Input.GetButton("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;

            anim.GetBool("isJumping");
            anim.SetBool("isJumping", true);

            // ȿ����
            PlaySound("JUMP");
            audioSource.Play();

        }

        /* void OnCollisionEnter2D(Collision2D collision)
         {
             if (collision.gameObject.CompareTag("Floor"))
             {
                 jumpCount = 0;
             }
         }
         */

        // �ӵ� ����
        if (Input.GetButtonUp("Horizontal"))
        {
            // rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            //Ű�� ����,x�� �ӵ� �⺻ 0.5��, y�� �ӵ��� �״��
        }

        // ���� �̹��� ������
        // Ű�� ������ ������, ���ʴ����� -1�Ǽ� �¿�ٲٱ�
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

           
        }

        // �ȴ� �ִϸ��̼�
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
        // �̵��ӵ� ����
        float h = Input.GetAxisRaw("Horizontal");       //h�� Ű�� ������ �Է� ������=1,����=-1
        rigid.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse); //h * �����ʰ��ؼ� ���� ��

        // �̵��ӵ� ����
        // ������ �ӵ� ����
        if (rigid.velocity.x > maxSpeed)         //x�ӵ��� maxSpeed ���� ũ��, �ӵ� maxSpeed�� ����
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        // ���� �ӵ� ����
        else if (rigid.velocity.x < maxSpeed * (-1))       //x�ӵ��� -maxSpeed ���� ������(�������� ����) �ӵ��� -maxSpeed�� ����
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        // Floor ����
        if (rigid.velocity.y < 0)
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
        if (collision.gameObject.tag == "Item")
        {
            // ������ ȹ��� ��� ����Ʈ
            bool isGem = collision.gameObject.name.Contains("ItemGem");
            bool isCarrot = collision.gameObject.name.Contains("ItemCarrot");
            bool isSnowflake = collision.gameObject.name.Contains("ItemSnowflake");
            bool isSilverCoin = collision.gameObject.name.Contains("ItemSilverCoin");
            

            if (isGem)
                PlatGameManager.stagePoint += 100;
            else if (isCarrot)
                PlatGameManager.stagePoint += 30;
            else if (isSnowflake)
                PlatGameManager.stagePoint += 30;
            else if (isSilverCoin)
                PlatGameManager.stagePoint += 30;

            // ������ �����
            collision.gameObject.SetActive(false);

            // ȿ����
            PlaySound("ITEM");
            audioSource.Play();
        }

        else if (collision.gameObject.tag == "Finish")
        {
            // ���� ����������!
            PlatGameManager.NextStage();

            // ȿ����
            PlaySound("FINISH");
            audioSource.Play();
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

        // ȿ����
        PlaySound("ATTACK");
        audioSource.Play();
    }

    // �ǰݽ�
    void OnDamaged(Vector2 targetPos)
    {
        // Hp���� ȣ�� : 1�� ����
        PlatGameManager.HpDown();

        /*
        // player ���̾� �ٲ� (Player -> PlaterDamaged ��!)
        gameObject.layer = 12;

        // �浹�� �������� ����( player �������ϰ� ����)
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        */

        // ������ ���� ��� �ݵ� ��
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);

        // �ִϸ��̼� ����
        anim.SetTrigger("doDamaged"); // ������ �ִϸ��̼� ���
        Invoke("OffDamaged", 3); // 3�ʰ� �������� ����

        // ȿ����
        PlaySound("DAMAGED");
        audioSource.Play();
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

        // ȿ����
        PlaySound("DIE");
        audioSource.Play();
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
    
    // ���� ����
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
    }
}

