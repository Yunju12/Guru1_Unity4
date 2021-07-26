using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatEnemyMove : MonoBehaviour
{

    Rigidbody2D rigid;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();

        Invoke("Think", 3);
    }

    
    void FixedUpdate()
    {
        // Move 기본 움직임 (왼쪽으로)
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 3);
        }
    }

    // 재귀함수
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }


}
