using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatEnemyMove : MonoBehaviour
{
    // 물리이동 변수
    Rigidbody2D rigid;

    // 애니메이션 변수
    Animator anim;

 
    SpriteRenderer spriteRenderer;

    // 물리 충돌 모양
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
        // 기본 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        // Floor 확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));  // 녹색, 아래방향 Ray 표시
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));        
        if (rayHit.collider == null) // 바닥이 비었을 경우 방향 전환
        {
            Turn();
        }
    }

    // 재귀함수
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

        // Recursive 재귀함수
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
