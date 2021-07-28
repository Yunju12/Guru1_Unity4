using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatEnemyMove : MonoBehaviour
{
    // �����̵� ����
    Rigidbody2D rigid;

    // �ִϸ��̼� ����
    Animator anim;

 
    SpriteRenderer spriteRenderer;

    // ���� �浹 ���
    BoxCollider2D boxCollider;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        Invoke("Think", 2);
    }

    
    void FixedUpdate()
    {
        // �⺻ ������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        // Floor Ȯ��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));  // ���, �Ʒ����� Ray ǥ��
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));        
        if (rayHit.collider == null) // �ٴ��� ����� ��� ���� ��ȯ
        {
            Turn();
        }
    }

    // ����Լ�
    void Think()
    {
        // Set Next Active
        nextMove = Random.Range(-1, 2);

        // Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        // Flip Sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // Recursive ����Լ�
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {

        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        boxCollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
