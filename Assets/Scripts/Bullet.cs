using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 속도 변수
    public float speed = 10;

    void Start()
    {
        
    }

    void Update()
    {
        // 옆으로 계속 이동하기
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
