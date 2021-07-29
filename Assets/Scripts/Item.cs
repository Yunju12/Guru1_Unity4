using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject i1;

    public GameObject i2;

    public GameObject i3;
    
    public GameObject item;

    // 속력 변수
    public float speed = 5;

    PlayerMove pm;

    void Start()
    {
        int randomValue = Random.Range(0, 3);

        if (randomValue == 0)
        {
            item = i1;
        }
        else if (randomValue == 1)
        {
            item = i2;
        }
        else
        {
            item = i3;
        }

        pm = GetComponent<PlayerMove>();
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
