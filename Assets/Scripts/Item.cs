using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 속력 변수
    public float speed = 5;

    void Start()
    {

    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        Vector3 speed = new Vector3(5, 5, 0);
        GetComponent<Rigidbody2D>().AddForce(speed);
    }
}